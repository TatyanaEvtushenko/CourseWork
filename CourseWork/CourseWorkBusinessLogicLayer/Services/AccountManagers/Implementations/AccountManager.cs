using System;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Options;
using CourseWork.BusinessLogicLayer.Services.ConverterExtensions;
using CourseWork.BusinessLogicLayer.Services.MessageSenders;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Enums.Configurations;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using CourseWork.DataLayer.Repositories.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CourseWork.BusinessLogicLayer.Services.AccountManagers.Implementations
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly Repository<UserInfo> _userInfoRepository;
        private readonly CloudinaryOptions _options;

        public AccountManager(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender, 
            IHttpContextAccessor contextAccessor,
            Repository<UserInfo> userInfoRepository, IOptions<CloudinaryOptions> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _contextAccessor = contextAccessor;
            _userInfoRepository = userInfoRepository;
            _options = options.Value;
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
            var user = _userInfoRepository.FirstOrDefault(u => u.ApplicationUser.Email == email, u => u.ApplicationUser);
            if (user == null || !user.ApplicationUser.EmailConfirmed || user.IsBlocked)
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

        public DisplayableInfoViewModel[] GetDisplayableInfo(string[] userNames)
        {
            return ((UserInfoRepository) _userInfoRepository).GetDisplayableInfo(userNames).Select(item => 
                item.ConvertTo<DisplayableInfoViewModel>()).ToArray();
        }

        public DisplayableInfoViewModel GetUserDisplayableInfo(string username)
        {
            return GetDisplayableInfo(new[] {username}).SingleOrDefault();
        }

        private async Task<bool> TryLogin(UserInfo user, string password)
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
            if (!result.Succeeded)
            {
                return false;
            }
            await SendConfirmation(user);
            return true;
        }

        private async Task<bool> TryConfirmRegistration(ApplicationUser user, string code)
        {
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return result.Succeeded && await AddNewUser(user);
        }

        private async Task<bool> AddNewUser(ApplicationUser user)
        {
            await AddRole(user, UserRole.User);
            var userInfo = CreateBasicUserInfo(user.UserName);
            return _userInfoRepository.AddRange(userInfo);
        }

        private UserInfo CreateBasicUserInfo(string userName)
        {
            return new UserInfo
            {
                UserName = userName,
                IsBlocked = false,
                Status = UserStatus.WithoutConfirmation,
                LastLoginTime = DateTime.UtcNow,
                RegistrationTime = DateTime.UtcNow,
                Avatar = _options.DefaultUserAvatar
            };
        }

        private void UpdateLoginTime(UserInfo user)
        {
            user.LastLoginTime = DateTime.UtcNow;
            _userInfoRepository.UpdateRange(user);
        }

        private async Task SendConfirmation(ApplicationUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = GetCallBackConfirmUrl(user.Id, code);
            await _emailSender.SendEmailAsync(user.Email, "Confirm your account", GetMessageToSendConfirmLink(callbackUrl));
        }

        private string GetCallBackConfirmUrl(string userId, string code) =>
            $"{GetBaseUrl()}/api/Account/ConfirmRegistration?userId={userId}&code={System.Net.WebUtility.UrlEncode(code)}";

        private string GetBaseUrl() =>
            $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}";

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
    }
}
