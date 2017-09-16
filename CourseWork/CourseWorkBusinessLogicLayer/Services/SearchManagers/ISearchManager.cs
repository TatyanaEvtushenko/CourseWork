using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.SearchManagers
{
    public interface ISearchManager
    {
        IEnumerable<ProjectItemViewModel> Search(string query);

        bool AddProjectToIndex(Project project);

        bool AddNewsToIndex(News news);

        bool RemoveProjectsFromIndex(Project[] projects);
    }
}
