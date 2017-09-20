using CourseWork.BusinessLogicLayer.Services.ProjectManagers;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using FluentScheduler;

namespace CourseWork.BusinessLogicLayer.Services.Scheduler
{
    public class SchedulerJob : IJob
    {
        private readonly Repository<Project> _projectRepository;
        private readonly IProjectManager _projectManager;

        public SchedulerJob(Repository<Project> projectRepository, IProjectManager projectManager)
        {
            _projectRepository = projectRepository;
            _projectManager = projectManager;
        }

        public void Execute()
        {
            UpdateProjectStatuses();
        }

        private void UpdateProjectStatuses()
        {
            var projects = _projectRepository.GetAll(project => project.Payments, project => project.FinancialPurposes);
            foreach (var project in projects)
            {
                _projectManager.ChangeProjectStatus(project, project.Payments, project.FinancialPurposes);
            }
            _projectRepository.UpdateRange(projects.ToArray());
        }
    }
}
