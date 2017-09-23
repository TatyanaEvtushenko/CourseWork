using System;
using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.Services.LocalizationManager;
using CourseWork.BusinessLogicLayer.ViewModels.LocalizationViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    public class LocalizationController : Controller
    {
        private readonly ILocalizationManager _localizationManager;
        private readonly IStringLocalizer<LocalizationController> _localizer;

        public LocalizationController(ILocalizationManager localizationManager, IStringLocalizer<LocalizationController> localizer)
        {
            _localizationManager = localizationManager;
            _localizer = localizer;
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

        [HttpGet]
        [Route("api/Localization/GetTranslations")]
        public Dictionary<string, string> GetTranslations([FromQuery] string[] keys)
        {
            var result = new Dictionary<string, string>();
            foreach (var key in keys)
            {
                result.Add(key, _localizer[key]);
            }
            return result;
        }
    }
}