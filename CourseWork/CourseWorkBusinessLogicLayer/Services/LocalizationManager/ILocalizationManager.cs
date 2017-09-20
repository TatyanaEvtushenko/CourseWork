using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.LocalizationViewModels;

namespace CourseWork.BusinessLogicLayer.Services.LocalizationManager
{
    public interface ILocalizationManager
    {
        SupportedAndCurrentLanguageViewModel GetSuppotedCultures();
    }
}
