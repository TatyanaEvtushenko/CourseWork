using FluentScheduler;

namespace CourseWork.Extensions.Scheduler
{
    public class SchedulerJobRegistry : Registry
    {
        public SchedulerJobRegistry()
        {
            Schedule<SchedulerJob>().ToRunNow().AndEvery(30).Minutes();
        }
    }
}
