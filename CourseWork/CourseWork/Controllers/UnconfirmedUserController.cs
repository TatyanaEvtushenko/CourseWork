using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private IRepository<UserInfo> _userRepository;

        public UnconfirmedUserController(IRepository<UserInfo> userRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("~/api/UnconfirmedUser/ConfirmAccount")]
        [Authorize(Roles = "User")]
        public IActionResult ConfirmAccount([FromBody] UserConfirmationViewModel model)
        {
            var user = _userRepository.Get(_userManager.GetUserId(HttpContext.User));
            if (user.Status == UserStatus.WithoutConfirmation && ModelState.IsValid && model != null)
            {
                RequestConfirmation(user, model);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        private void RequestConfirmation(UserInfo user, UserConfirmationViewModel model)
        {
            user.PassportScan = model.PassportScan;
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Description = model.Description;
            user.Status = UserStatus.AwaitingConfirmation;
            _userRepository.UpdateRange(user);
        }
    }
}