using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.SearchManagers
{
    public interface ISearchManager
    {
        IEnumerable<ProjectItemViewModel> Search(string query);

        bool AddProjectToIndex(Project project);

        bool RemoveProjectsFromIndex(Project[] projects);

        bool AddNewsToIndex(News news);

        bool RemoveNewsFromIndex(News news);

        bool AddCommentToIndex(Comment comment);

        bool RemoveCommentsFromIndex(Comment[] comments);
    }
}
