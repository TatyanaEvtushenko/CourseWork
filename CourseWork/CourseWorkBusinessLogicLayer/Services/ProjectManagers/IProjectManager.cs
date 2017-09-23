using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.PaymentViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.ProjectManagers
{
    public interface IProjectManager
    {
        IEnumerable<ProjectItemViewModel> GetLastCreatedProjects();

        IEnumerable<ProjectItemViewModel> GetFinancedProjects();

        bool AddProject(ProjectFormViewModel projectForm, string awardName);

        IEnumerable<ProjectItemViewModel> GetUserProjects(string userName);

        IEnumerable<ProjectItemViewModel> GetCurrentUserProjects();

        ProjectViewModel GetProject(string projectId);

        ProjectEditorFormViewModel GetProjectEditorForm(string projectId);

        bool UpdateProject(ProjectFormViewModel projectForm);

        void ChangeProjectStatus(Project project);

        IEnumerable<ProjectItemViewModel> GetUserSubscribedProjects(string userName);

        IEnumerable<ProjectItemViewModel> GetSubscribedProjects();

        bool AddPayment(PaymentFormViewModel paymentForm, string awardNamePayment, string awardNameReceived);

        double GetProjectRating(Project project);
    }
}
