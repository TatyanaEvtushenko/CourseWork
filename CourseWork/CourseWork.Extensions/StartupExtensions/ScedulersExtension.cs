using CourseWork.Extensions.Scheduler;
using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;

namespace CourseWork.Extensions.StartupExtensions
{
    public static class ScedulersExtension
    {
        public static void RunScheduler(this IServiceCollection services)
        {
            services.AddScoped<SchedulerJob, SchedulerJob>();
            JobManager.JobFactory = new SchedulerJobFactory(services);
            JobManager.Initialize(new SchedulerJobRegistry());
        }
    }
}
