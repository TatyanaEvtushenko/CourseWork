using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.ColorViewModels;
using Microsoft.AspNetCore.Http;

namespace CourseWork.BusinessLogicLayer.Services.ColorManagers.Implementations
{
    public class ColorManager : IColorManager
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ColorManager(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public SupportedAndCurrentColorViewModel GetSupportedColors()
        {
            return new SupportedAndCurrentColorViewModel
            {
                SupportedColors = new List<ColorViewModel>(
                new[] {
                    new ColorViewModel {Name = "light", Key = "LIGHT"},
                    new ColorViewModel {Name = "dark", Key = "DARK"}
                }),
                CurrentColor = _contextAccessor.HttpContext.Request.Cookies["color"] ?? "light"
            };
        }
    }
}
