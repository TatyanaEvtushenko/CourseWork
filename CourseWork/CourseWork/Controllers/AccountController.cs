using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Services.AccountManagers;
using CourseWork.BusinessLogicLayer.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
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
        [Authorize]
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

        [HttpGet]
        [Route("api/Account/IsAdmin")]
        public async Task<bool> IsAdmin()
        {
            return await _accountManager.IsAdmin();
        }

        [HttpGet]
        [Route("api/Account/IsConfirmedUser")]
        public async Task<bool> IsConfirmedUser()
        {
            return await _accountManager.IsConfirmedUser();
        }

        [HttpGet]
        [Route("api/Account/IsUser")]
        public async Task<bool> IsUser()
        {
            return await _accountManager.IsUser();
        }
    }
}