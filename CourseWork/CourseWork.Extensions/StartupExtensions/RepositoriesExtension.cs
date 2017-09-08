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
            services.AddScoped<Repository<Project>, ProjectRepository>();
            services.AddScoped<Repository<News>, NewsRepository>();
            services.AddScoped<Repository<FinancialPurpose>, FinancialPurposeRepository>();
            services.AddScoped<Repository<UserInfo>, UserInfoRepository>();
            services.AddScoped<Repository<Payment>, PaymentRepository>();
            services.AddScoped<Repository<ProjectSubscriber>, ProjectSubscriberRepository>();
            services.AddScoped<Repository<ApplicationUser>, ApplicationUserRepository>();
            services.AddScoped<Repository<Raiting>, RaitingRepository>();
        }
    }
}
