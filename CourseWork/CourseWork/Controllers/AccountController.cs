using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Services.AccountManagers;
using CourseWork.BusinessLogicLayer.ViewModels.AccountViewModels;
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
        public async Task<bool> Register([FromBody]RegisterViewModel user)
        {
            return false; //await _accountManager.Register(user.userName, user.Email, user.Password, user.PasswordConfirmation);
        }
    }
}