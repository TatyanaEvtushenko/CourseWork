using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using CourseWork.DataLayer.Repositories.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace CourseWork.Extensions.StartupExtensions
{
    public static class RepositoriesExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Tag>, TagRepository>();
            services.AddScoped<IRepository<TagInProject>, TagInProjectRepository>();
        }
    }
}
