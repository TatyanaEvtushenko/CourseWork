using System;
using System.Threading.Tasks;
using CourseWork.DataLayer.Models;
using CourseWork.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CourseWork.BusinessLogicLayer.Services.AccountManagers.Implementations
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly string _externalCookieScheme;

        public AccountManager(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<IdentityCookieOptions> identityCookieOptions,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<AccountManager>();
        }

        private async Task<bool> Register(string userName, string email, string password)
        {
            var user = new ApplicationUser { UserName = userName, Email = email};
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded) return false;
            await SendConfirmation(user);
            await _userManager.AddToRoleAsync(user, "User");
            return true;
        }

        private async Task SendConfirmation(ApplicationUser user)
        {
            //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //var callbackUrl = new Uri();  Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            //await _emailSender.SendEmailAsync(user.Email, "Confirm your account",
            //    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
        }
    }
}
