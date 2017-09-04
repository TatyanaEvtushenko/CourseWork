using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Services.AccountConfirmationManagers;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    [Route("api/UnconfirmedUser")]
    public class UnconfirmedUserController : Controller
    {
        private IAccountConfirmationManager _accountConfirmationManager;
        private UserManager<ApplicationUser> _userManager;

        public UnconfirmedUserController(IAccountConfirmationManager accountConfirmationManager, UserManager<ApplicationUser> userManager)
        {
            _accountConfirmationManager = accountConfirmationManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("~/api/UnconfirmedUser/ConfirmAccount")]
        [Authorize(Roles = "User")]
        public IActionResult ConfirmAccount([FromBody] UserConfirmationViewModel model)
        {
            return _accountConfirmationManager.ConfirmAccount(_userManager.GetUserId(HttpContext.User), model) ?
                (IActionResult) Ok() : BadRequest();
        }

        
    }
}