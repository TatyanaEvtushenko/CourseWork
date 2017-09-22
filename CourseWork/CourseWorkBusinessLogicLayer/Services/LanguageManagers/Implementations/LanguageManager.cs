using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace CourseWork.BusinessLogicLayer.Services.LanguageManagers.Implementations
{
    public class LanguageManager : ILanguageManager
    {
        private readonly RequestLocalizationOptions _options;

        public LanguageManager(IOptions<RequestLocalizationOptions> options)
        {
            _options = options.Value;
        }

        public List<SelectListItem> GetSupportedCultures()
        {
            var cultureItems = _options.SupportedUICultures.Select(c =>
                new SelectListItem { Value = c.Name, Text = c.DisplayName }).ToList();
            return cultureItems;
        }
    }
}
