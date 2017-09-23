using System;
using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.Services.LanguageManagers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    public class LocalizationController : Controller
    {
        private readonly ILanguageManager _languageManager;

        public LocalizationController(ILanguageManager languageManager)
        {
            _languageManager = languageManager;
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                                    new CookieOptions { Expires = DateTimeOffset.Now.AddYears(1) });
            return Redirect(returnUrl);
        }

        [HttpGet]
        public List<SelectListItem> GetSupportedCultures()
        {
            return _languageManager.GetSupportedCultures();
        }
    }
}