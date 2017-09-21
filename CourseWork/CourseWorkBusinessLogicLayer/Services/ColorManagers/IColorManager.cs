using CourseWork.BusinessLogicLayer.ViewModels.ColorViewModels;

namespace CourseWork.BusinessLogicLayer.Services.ColorManagers
{
    public interface IColorManager
    {
        SupportedAndCurrentColorViewModel GetSupportedColors();
    }
}
