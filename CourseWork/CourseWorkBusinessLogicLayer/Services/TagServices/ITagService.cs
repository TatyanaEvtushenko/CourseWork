using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.TagViewModels;

namespace CourseWork.BusinessLogicLayer.Services.TagServices
{
    public interface ITagService
    {
        IEnumerable<TagViewModel> GetAllTagViewModels();
    }
}
