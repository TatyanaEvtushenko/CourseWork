using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Services.ColorManagers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using  CourseWork.BusinessLogicLayer.ViewModels.ColorViewModels;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    public class ColorsController : Controller
    {
        private IColorManager _colorManager;

        public ColorsController(IColorManager colorManager)
        {
            _colorManager = colorManager;
        }

        [HttpPost]
        [Route("api/Colors/SetColor")]
        public void SetColor([FromBody] string colorName)
        {
            Response.Cookies.Append("color", colorName,
                new CookieOptions { Expires = DateTimeOffset.Now.AddYears(1) });
        }

        [HttpGet]
        [Route("api/Colors/GetSupportedColors")]
        public SupportedAndCurrentColorViewModel GetSupportedColors()
        {
            return _colorManager.GetSupportedColors();
        }
    }
}