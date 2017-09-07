using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Services.AccountManagers;
using CourseWork.BusinessLogicLayer.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpPost]
        [Route("api/Account/Register")]
        [AllowAnonymous]
        public async Task<bool> Register([FromBody]RegisterViewModel user)
        {
            return await _accountManager.Register(user.UserName, user.Email, user.Password);
        }

        [HttpPost]
        [Route("api/Account/Login")]
        [AllowAnonymous]
        public async Task<bool> Login([FromBody]LoginViewModel user)
        {
            return await _accountManager.Login(user.Email, user.Password);
        }

        [HttpGet]
        [Route("api/Account/LogOut")]
        public async Task LogOut()
        {
            await _accountManager.Logout();
        }

        [HttpGet]
        [Route("api/Account/ConfirmRegistration")]
        [AllowAnonymous]
        public async Task<bool> ConfirmRegistration([FromQuery] ConfirmationRegistrationViewModel confirmation)
        {
            return await _accountManager.ConfirmRegistration(confirmation.UserId, confirmation.Code);
        }
    }
}