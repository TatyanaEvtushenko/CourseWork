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
            services.AddScoped<Repository<Tag>, TagRepository>();
            services.AddScoped<Repository<TagInProject>, TagInProjectRepository>();
            services.AddScoped<Repository<UserInfo>, UserInfoRepository>();
        }
    }
}
