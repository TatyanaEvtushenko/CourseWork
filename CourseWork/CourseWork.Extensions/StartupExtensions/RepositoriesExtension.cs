using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using CourseWork.DataLayer.Repositories.Implementations.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace CourseWork.Extensions.StartupExtensions
{
    public static class RepositoriesExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Tag>, TagRepository>();
            services.AddScoped<IRepository<Project>, ProjectRepository>();
            services.AddScoped<IRepository<News>, NewsRepository>();
            services.AddScoped<IRepository<FinancialPurpose>, FinancialPurposeRepository>();
            services.AddScoped<IRepository<UserInfo>, UserInfoRepository>();
            services.AddScoped<IRepository<Payment>, PaymentRepository>();
            services.AddScoped<IRepository<ProjectSubscriber>, ProjectSubscriberRepository>();
            services.AddScoped<IRepository<ApplicationUser>, ApplicationUserRepository>();
	        services.AddScoped<IRepository<Rating>, RatingRepository>();
	        services.AddScoped<IRepository<Comment>, CommentRepository>();
	        services.AddScoped<IRepository<Message>, MessageRepository>();
        }
    }
}
