using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Services.AdminManagers;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminManager _adminManager;

        public AdminController(IAdminManager adminManager)
        {
            _adminManager = adminManager;
        }

        [HttpGet]
        [Route("api/Admin/GetAllUsers")]
        public UserListItemViewModel[] GetAllUsers()
        {
            return _adminManager.GetAllUsers();
        }

        [HttpGet]
        [Route("api/Admin/GetFilteredUsers")]
        public UserListItemViewModel[] GetFilteredUsers([FromQuery] bool confirmed, [FromQuery] bool requested, [FromQuery] bool unconfirmed)
        {
            return _adminManager.GetFilteredUsers(confirmed, requested, unconfirmed);
        }

        [HttpGet]
        [Route("api/Admin/GetPersonalInfo")]
        public UserConfirmationViewModel GetPersonalInfo([FromQuery] string userName)
        {
            return _adminManager.GetPersonalInfo(userName);
        }

        [HttpGet]
        [Route("api/Admin/RespondToConfirmation")]
        public bool RespondToConfirmation([FromQuery] string userName, [FromQuery] bool accept)
        {
            return _adminManager.RespondToConfirmation(userName, accept);
        }
    }
}