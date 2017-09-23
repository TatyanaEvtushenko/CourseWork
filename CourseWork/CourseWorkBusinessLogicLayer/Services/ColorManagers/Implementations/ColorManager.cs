using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.Options;
using CourseWork.BusinessLogicLayer.ViewModels.ColorViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CourseWork.BusinessLogicLayer.Services.ColorManagers.Implementations
{
    public class ColorManager : IColorManager
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ColorOptions _options;

        public ColorManager(IHttpContextAccessor contextAccessor, IOptions<ColorOptions> options)
        {
            _contextAccessor = contextAccessor;
            _options = options.Value;
        }

        public SupportedAndCurrentColorViewModel GetSupportedColors()
        {
            return new SupportedAndCurrentColorViewModel
            {
                SupportedColors = new List<ColorViewModel>(
                new[] {
                    new ColorViewModel {Name = _options.LightStyle, Key = _options.LightKey},
                    new ColorViewModel {Name = _options.DarkStyle, Key = _options.DarkKey}
                }),
                CurrentColor = _contextAccessor.HttpContext.Request.Cookies["color"] ?? _options.LightStyle
            };
        }
    }
}
