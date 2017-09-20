using System;
using CourseWork.BusinessLogicLayer.Services.LocalizationManager;
using CourseWork.BusinessLogicLayer.ViewModels.LocalizationViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    public class LocalizationController : Controller
    {
        private readonly ILocalizationManager _localizationManager;

        public LocalizationController(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
        }

        [HttpPost]
        [Route("api/Localization/SetLanguage")]
        public void SetLanguage([FromBody] string cultureName)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cultureName)),
                                    new CookieOptions { Expires = DateTimeOffset.Now.AddYears(1) });
        }

        [HttpGet]
        [Route("api/Localization/GetSupportedCultures")]
        public SupportedAndCurrentLanguageViewModel GetSupportedCultures()
        {
            return _localizationManager.GetSuppotedCultures();
        }
    }
}