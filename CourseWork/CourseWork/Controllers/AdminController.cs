using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [HttpGet]
        [Route("api/Admin/GetAllUsers")]
        public List<UserListItemViewModel> GetAllUsers()
        {
            return null;
        }
    }
}