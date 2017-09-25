using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseWork.BusinessLogicLayer.ViewModels.LocalizationViewModels
{
    public class SupportedAndCurrentLanguageViewModel
    {
        public string CurrentLanguage { get; set; } 

        public List<LanguageViewModel> SupportedLanguages { get; set; }
    }
}
