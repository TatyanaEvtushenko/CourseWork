using CourseWork.BusinessLogicLayer.Services.AccountConfirmationManagers;
using CourseWork.BusinessLogicLayer.Services.AccountConfirmationManagers.Implementations;
using CourseWork.BusinessLogicLayer.Services.AccountManagers;
using CourseWork.BusinessLogicLayer.Services.AccountManagers.Implementations;
using CourseWork.BusinessLogicLayer.Services.FinancialPurposeManagers;
using CourseWork.BusinessLogicLayer.Services.FinancialPurposeManagers.Implementations;
using CourseWork.BusinessLogicLayer.Services.MessageSenders;
using CourseWork.BusinessLogicLayer.Services.MessageSenders.Implementations;
using CourseWork.BusinessLogicLayer.Services.PhotoManagers;
using CourseWork.BusinessLogicLayer.Services.PhotoManagers.Implementations;
using CourseWork.BusinessLogicLayer.Services.TagServices;
using CourseWork.BusinessLogicLayer.Services.TagServices.Implementations;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.Services.UserManagers.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace CourseWork.Extensions.StartupExtensions
{
    public static class ServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IFinancialPurposeManager, FinancialPurposeManager>();
            services.AddScoped<IAccountConfirmationManager, AccountConfirmationManager>();
            services.AddScoped<IPhotoManager, PhotoManager>();
        }
    }
}
