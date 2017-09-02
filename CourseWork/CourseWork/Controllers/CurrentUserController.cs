using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.CurrentUserViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    [Route("api/CurrentUser")]
    public class CurrentUserController : Controller
    {
        private readonly IUserManager _userManager;

        public CurrentUserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public CurrentUserViewModel GetCurrentUserInfo()
        {
            return _userManager.GetCurrentUserInfo();
        }
    }
}