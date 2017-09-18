using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.ProjectManagers
{
    public interface IProjectManager
    {
        IEnumerable<ProjectItemViewModel> GetLastCreatedProjects();

        IEnumerable<ProjectItemViewModel> GetFinancedProjects();

        bool AddProject(ProjectFormViewModel projectForm);

        IEnumerable<ProjectItemViewModel> GetUserProjects();

        IEnumerable<ProjectItemViewModel> GetProjects(string username);

        ProjectViewModel GetProject(string projectId);

        void ChangeRating(RatingViewModel ratingForm);

        ProjectEditorFormViewModel GetProjectEditorForm(string projectId);

        bool UpdateProject(ProjectFormViewModel projectForm);

        void ChangeProjectStatus(Project project, IEnumerable<Payment> payments,
            IEnumerable<FinancialPurpose> purposes);

        IEnumerable<ProjectItemViewModel> GetUserSubscribedProjects();

        IEnumerable<ProjectItemViewModel> GetSubscribedProjects(string username);

        string GetProjectName(string projectId);
    }
}
