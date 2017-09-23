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
        public UserListItemViewModel[] GetFilteredUsers([FromQuery] FilterRequestViewModel model)
        {
            return _adminManager.GetFilteredUsers(model);
        }

        [HttpGet]
        [Route("api/Admin/GetPersonalInfo")]
        public UserConfirmationViewModel GetPersonalInfo([FromQuery] string userName)
        {
            return _adminManager.GetPersonalInfo(userName);
        }

        [HttpGet]
        [Route("api/Admin/RespondToConfirmation")]
        public async Task<bool> RespondToConfirmation([FromQuery] string userName, [FromQuery] bool accept)
        {
            var message = accept ? "APPROVECONFIRMATION" : "DECLINECONFIRMATION";
            return await _adminManager.RespondToConfirmation(userName, accept, message);
        }

        [HttpGet]
        [Route("api/Admin/SortByField")]
        public UserListItemViewModel[] SortByField([FromQuery] string fieldName, [FromQuery] bool ascending, [FromQuery] FilterRequestViewModel filters)
        {
            return _adminManager.SortByField(fieldName, ascending, filters);
        }

        [HttpGet]
        [Route("api/Admin/BlockUnblock")]
        public bool BlockUnblock([FromQuery] string[] usersToBlock)
        {
            return _adminManager.BlockUnblock(usersToBlock);
        }

        [HttpGet]
        [Route("api/Admin/Delete")]
        public bool Delete([FromQuery] string[] usersToDelete, [FromQuery] bool withCommentsAndRaitings)
        {
            return _adminManager.Delete(usersToDelete, withCommentsAndRaitings);
        }
    }
}