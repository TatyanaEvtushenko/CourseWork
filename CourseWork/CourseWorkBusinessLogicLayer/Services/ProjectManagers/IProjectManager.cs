using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;

namespace CourseWork.BusinessLogicLayer.Services.ProjectManagers
{
    public interface IProjectManager
    {
        IEnumerable<ProjectItemViewModel> GetLastCreatedProjects();

        IEnumerable<ProjectItemViewModel> GetFinancedProjects();

        bool AddProject(ProjectFormViewModel projectForm);
    }
}
