using System;
using System.Diagnostics;
using CourseWork.BusinessLogicLayer.Services.ProjectManagers;
using FluentScheduler;

namespace CourseWork.Extensions.Scheduler
{
    public class SchedulerJob : IJob
    {
        private readonly IProjectManager _projectManager;

        public SchedulerJob(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        public void Execute()
        {
            _projectManager.UpdateExistedProjects();
        }
    }
}
