using CourseWork.BusinessLogicLayer.Services.ProjectManagers;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using FluentScheduler;

namespace CourseWork.BusinessLogicLayer.Services.Scheduler
{
    public class SchedulerJob : IJob
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IProjectManager _projectManager;

        public SchedulerJob(IRepository<Project> projectRepository, IProjectManager projectManager)
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
                _projectManager.ChangeProjectStatus(project);
            }
            _projectRepository.UpdateRange(projects.ToArray());
        }
    }
}
