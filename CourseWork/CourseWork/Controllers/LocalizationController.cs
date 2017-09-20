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
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                                    new CookieOptions { Expires = DateTimeOffset.Now.AddYears(1) });
            return Redirect(returnUrl);
        }

        [HttpGet]
        [Route("api/Localization/GetSupportedCultures")]
        public SupportedAndCurrentLanguageViewModel GetSupportedCultures()
        {
            return _localizationManager.GetSuppotedCultures();
        }
    }
}