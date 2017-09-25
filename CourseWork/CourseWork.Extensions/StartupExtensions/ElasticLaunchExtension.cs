using Microsoft.Extensions.DependencyInjection;

namespace CourseWork.Extensions.StartupExtensions
{
    public static class ElasticLaunchExtension
    {
        public static void LaunchElastic(this IServiceCollection services, string launcherPath)
        {
            System.Diagnostics.Process.Start(launcherPath);
        }
    }
}