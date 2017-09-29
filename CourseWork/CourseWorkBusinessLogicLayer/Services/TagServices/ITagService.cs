using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.TagViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.TagServices
{
    public interface ITagService
    {
        IEnumerable<TagViewModel> GetAllTagViewModels();

        IEnumerable<string> GetAllTagNames();

        IEnumerable<string> GetProjectTags(Project project);

        IEnumerable<Tag> ConvertStringsToTags(IEnumerable<string> tags, string projectId);
    }
}
