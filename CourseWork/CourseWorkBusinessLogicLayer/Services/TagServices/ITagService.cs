using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.TagViewModels;

namespace CourseWork.BusinessLogicLayer.Services.TagServices
{
    public interface ITagService
    {
        IEnumerable<TagViewModel> GetAllTagViewModels();

        IEnumerable<string> GetAllTagNames();

        bool AddTagsInProject(IEnumerable<string> tagsToAdding, string projectId);

        IEnumerable<string> GetProjectTags(string projectId);
    }
}
