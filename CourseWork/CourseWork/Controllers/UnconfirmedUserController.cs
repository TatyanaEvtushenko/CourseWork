using CourseWork.BusinessLogicLayer.Services.AccountConfirmationManagers;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    public class UnconfirmedUserController : Controller
    {
        private readonly IAccountConfirmationManager _accountConfirmationManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UnconfirmedUserController(IAccountConfirmationManager accountConfirmationManager, UserManager<ApplicationUser> userManager)
        {
            _accountConfirmationManager = accountConfirmationManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("api/UnconfirmedUser/ConfirmAccount")]
        [Authorize(Roles = "User")]
        public bool ConfirmAccount([FromBody] UserConfirmationViewModel model)
        {
            return _accountConfirmationManager.ConfirmAccount(_userManager.GetUserId(HttpContext.User), model);
        }
    }
}