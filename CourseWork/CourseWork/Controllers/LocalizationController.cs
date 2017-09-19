using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    public class LocalizationController : Controller
    {
        private readonly RequestLocalizationOptions _options;

        public LocalizationController(IOptions<RequestLocalizationOptions> options)
        {
            _options = options.Value;
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
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var cultureItems = _options.SupportedUICultures.Select(c =>
                new SelectListItem {Value = c.Name, Text = c.DisplayName}).ToList();
            return cultureItems;
        }
    }
}