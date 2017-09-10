using System;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Services.MessageSenders;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Enums.Configurations;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CourseWork.BusinessLogicLayer.Services.AccountManagers.Implementations
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly Repository<UserInfo> _userInfoRepository;

        public AccountManager(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender, 
            IHttpContextAccessor contextAccessor,
            Repository<UserInfo> userInfoRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _contextAccessor = contextAccessor;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<bool> Register(string userName, string email, string password)
        {
            var user = new ApplicationUser {UserName = userName ?? email, Email = email};
            return await TryRegister(user, password);
        }

        public async Task<bool> ConfirmRegistration(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return false;
            }
            var user = await _userManager.FindByIdAsync(userId);
            return user != null && await TryConfirmRegistration(user, code);
        }

        public async Task<bool> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.IsEmailConfirmedAsync(user) || _userInfoRepository.Get(user.UserName).IsBlocked)
            {
                return false;
            }
            return await TryLogin(user, password);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task AddRole(string userName, UserRole role)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await AddRole(user, role);
        }

        public async Task RemoveRole(string userName, UserRole role)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await RemoveRole(user, role);
        }

        private async Task<bool> TryLogin(ApplicationUser user, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, true, false);
            if (result.Succeeded)
            {
                UpdateLoginTime(user);
            }
            return result.Succeeded;
        }

        private async Task<bool> TryRegister(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await SendConfirmation(user);
            }
            return result.Succeeded;
        }

        private async Task<bool> TryConfirmRegistration(ApplicationUser user, string code)
        {
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                var userInfo = CreateBasicUserInfo(user.UserName);
                if (!_userInfoRepository.AddRange(userInfo))
                    return false;
                await AddRole(user, UserRole.User);
            }
            return result.Succeeded;
        }

        private static UserInfo CreateBasicUserInfo(string userName)
        {
            return new UserInfo
            {
                UserName = userName,
                IsBlocked = false,
                ProjectNumber = 0,
                Raiting = 0,
                Status = UserStatus.WithoutConfirmation,
                LastLoginTime = DateTime.Now,
                RegistrationTime = DateTime.Now
            };
        }

        private void UpdateLoginTime(ApplicationUser user)
        {
            var userInfo = _userInfoRepository.Get(user.UserName);
            userInfo.LastLoginTime = DateTime.Now;
            _userInfoRepository.UpdateRange(userInfo);
        }

        private async Task SendConfirmation(ApplicationUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = $"{GetBaseUrl()}/api/Account/ConfirmRegistration?userId={user.Id}&code={System.Net.WebUtility.UrlEncode(code)}";
            await _emailSender.SendEmailAsync(user.Email, "Confirm your account", GetMessageToSendConfirmLink(callbackUrl));
        }

        private string GetBaseUrl()
        {
            return $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}";
        }

        private async Task AddRole(ApplicationUser user, UserRole role)
        {
            await _userManager.AddToRoleAsync(user, EnumConfiguration.RoleNames[role]);
        }

        private async Task RemoveRole(ApplicationUser user, UserRole role)
        {
            await _userManager.RemoveFromRoleAsync(user, EnumConfiguration.RoleNames[role]);
        }

        private static string GetMessageToSendConfirmLink(string url) =>
            $"Please confirm your account by clicking this link: <a href=\"{url}\">link</a>";

        private async Task<bool> IsInRole(UserRole role)
        {
            var user = _contextAccessor.HttpContext.User.Identity;
            if (!user.IsAuthenticated)
            {
                return false;
            }
            var applicationUser = await _userManager.FindByNameAsync(user.Name);
            return await CheckRoleQueue(applicationUser, role);
        }

        private async Task<bool> CheckRoleQueue(ApplicationUser user, UserRole role)
        {
            var isInRole = false;
            for (var i = role; i <= UserRole.Admin && !isInRole; i++)
            {
                isInRole = await _userManager.IsInRoleAsync(user, EnumConfiguration.RoleNames[i]);
            }
            return isInRole;
        }
    }
}
