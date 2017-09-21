using System;
using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;

namespace CourseWork.BusinessLogicLayer.Services.Scheduler
{
    public class SchedulerJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SchedulerJobFactory(IServiceCollection services)
        {
            _serviceProvider = services.BuildServiceProvider();
        }

        public IJob GetJobInstance<T>() where T : IJob
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
