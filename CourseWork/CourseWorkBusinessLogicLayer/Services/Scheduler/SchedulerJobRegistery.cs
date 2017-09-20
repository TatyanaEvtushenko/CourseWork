using FluentScheduler;

namespace CourseWork.BusinessLogicLayer.Services.Scheduler
{
    public class SchedulerJobRegistry : Registry
    {
        public SchedulerJobRegistry()
        {
            Schedule<SchedulerJob>().ToRunNow().AndEvery(1).Days().At(0, 0);
        }
    }
}
