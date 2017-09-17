using CourseWork.BusinessLogicLayer.Services.Scedulers;
using FluentScheduler;

namespace CourseWork.Extensions.Scheduler
{
    public class SchedulerJob : IJob
    {
        private readonly IScheduler _scheduler;

        public SchedulerJob(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public void Execute()
        {
            _scheduler.UpdateData();
        }
    }
}
