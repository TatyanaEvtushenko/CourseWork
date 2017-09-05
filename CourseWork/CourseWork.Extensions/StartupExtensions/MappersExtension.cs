using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.Mappers.Implementations;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CourseWork.Extensions.StartupExtensions
{
    public static class MappersExtension
    {
        public static void AddMappers(this IServiceCollection services)
        {
            services.AddScoped<IMapper<ProjectItemViewModel, Project>, ProjectItemViewModelToProjectMapper>();
            services.AddScoped<IMapper<ProjectFormViewModel, Project>, ProjectFormViewModelToProjectMapper>();
            services.AddScoped<IMapper<FinancialPurposeViewModel, FinancialPurpose>, FinancialPurposeViewModelToFinancialPurposeMapper>();
        }
    }
}
