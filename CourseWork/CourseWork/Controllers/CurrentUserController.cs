using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Services.PhotoManagers;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.AccountViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.CurrentUserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    public class CurrentUserController : Controller
    {
        private readonly IUserManager _userManager;

        public CurrentUserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("api/CurrentUser/GetCurrentUserInfo")]
        public async Task<CurrentUserViewModel> GetCurrentUserInfo()
        {
            return await _userManager.GetCurrentUserInfo();
        }

        [HttpPost]
        [Route("api/CurrentUser/Edit")]
        [Authorize]
        public void Edit([FromBody] AccountEditViewModel newInfo)
        {
            _userManager.Edit(newInfo);
        }

        [HttpPost]
        [Route("api/CurrentUser/ChangeAvatar")]
        [Authorize]
        public KeyValuePair<string, string> ChangeAvatar([FromBody] string newAvatarB64)
        {
            return new KeyValuePair<string, string>("", _userManager.ChangeAvatar(newAvatarB64));
        }
    }
}