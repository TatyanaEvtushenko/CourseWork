using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ElasticSearch.Documents;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.SearchManagers
{
    public interface ISearchManager
    {
        IEnumerable<ProjectItemViewModel> Search(string query);

        bool AddToIndex(Project project);
    }
}
