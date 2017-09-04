using CourseWork.BusinessLogicLayer.Services.SettingManagers;
using CourseWork.BusinessLogicLayer.ViewModels.SettingViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    public class SettingController : Controller
    {
        private readonly ISettingManager _settingManager;

        public SettingController(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        [HttpGet]
        [Route("api/Setting/GetAllRoles")]
        public RoleNamesViewModel GetAllRoles()
        {
            return _settingManager.GetRoles();
        }
    }
}