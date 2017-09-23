using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseWork.BusinessLogicLayer.Services.LanguageManagers
{
    public interface ILanguageManager
    {
        List<SelectListItem> GetSupportedCultures();
    }
}
