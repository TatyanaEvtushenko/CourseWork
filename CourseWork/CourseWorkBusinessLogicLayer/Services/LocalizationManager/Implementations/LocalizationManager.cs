using System.Linq;
using CourseWork.BusinessLogicLayer.ViewModels.LocalizationViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Nest;

namespace CourseWork.BusinessLogicLayer.Services.LocalizationManager.Implementations
{
    public class LocalizationManager : ILocalizationManager
    {
        private readonly RequestLocalizationOptions _options;
        private readonly IHttpContextAccessor _contextAccessor;

        public LocalizationManager(IOptions<RequestLocalizationOptions> options, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _options = options.Value;
        }

        public SupportedAndCurrentLanguageViewModel GetSuppotedCultures()
        {
            var requestCulture = _contextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            var cultureItems = _options.SupportedUICultures
                .Select(c => new LanguageViewModel { Name = c.Name, DisplayName = c.DisplayName })
                .ToList();
            return new SupportedAndCurrentLanguageViewModel
            {
                CurrentLanguage = requestCulture.RequestCulture.UICulture.Name,
                SupportedLanguages = cultureItems
            };
        }
    }
}
