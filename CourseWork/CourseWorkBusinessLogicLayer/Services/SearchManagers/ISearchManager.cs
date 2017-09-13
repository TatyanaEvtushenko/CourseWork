using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;

namespace CourseWork.BusinessLogicLayer.Services.SearchManagers
{
    public interface ISearchManager
    {
        IEnumerable<ProjectItemViewModel> Search(string query);
    }
}
