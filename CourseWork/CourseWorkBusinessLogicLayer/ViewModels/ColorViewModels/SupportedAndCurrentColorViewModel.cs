using System.Collections.Generic;

namespace CourseWork.BusinessLogicLayer.ViewModels.ColorViewModels
{
    public class SupportedAndCurrentColorViewModel
    {
        public string CurrentColor { get; set; }

        public List<ColorViewModel> SupportedColors { get; set; }
    }
}
