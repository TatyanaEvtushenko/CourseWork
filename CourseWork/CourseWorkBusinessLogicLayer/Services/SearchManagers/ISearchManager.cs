using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ElasticSearch.Documents;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;

namespace CourseWork.BusinessLogicLayer.Services.SearchManagers
{
    public interface ISearchManager
    {
        IEnumerable<ProjectSearchNote> Search(string query);
    }
}
