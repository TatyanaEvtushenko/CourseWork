using System;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Services.MessageSenders;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Enums.Configurations;
using CourseWork.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CourseWork.BusinessLogicLayer.Services.AccountManagers.Implementations
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountManager(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
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
            if (user != null && !await _userManager.IsEmailConfirmedAsync(user))
            {
                return false;
            }
            var result = await _signInManager.PasswordSignInAsync(email, password, true, lockoutOnFailure: false);
            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
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
                await AddRole(user, UserRole.User);
            }
            return result.Succeeded;
        }

        private async Task SendConfirmation(ApplicationUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = $"{ApplicationEnvironment.ApplicationBasePath}/api/Account/ConfirmRegistration?userId={user.Id}&code={code}";
            //Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme));
            await _emailSender.SendEmailAsync(user.Email, "Confirm your account", GetMessageToSendConfirmLink(callbackUrl));
        }

        private async Task AddRole(ApplicationUser user, UserRole role)
        {
            await _userManager.AddToRoleAsync(user, EnumConfiguration.RoleNames[role]);
        }

        private static string GetMessageToSendConfirmLink(string url) =>
            $"Please confirm your account by clicking this link: <a href='{url}'>link</a>";
    }
}
