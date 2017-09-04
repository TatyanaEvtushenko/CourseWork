using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.Mappers.Implementations;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.SettingViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CourseWork.Extensions.StartupExtensions
{
    public static class MappersExtension
    {
        public static void AddMappers(this IServiceCollection services)
        {
            services.AddScoped<IMapper<RoleNamesViewModel, Dictionary<UserRole, string>>, RoleNamesViewModelToRoleNamesMapper>();
            services.AddScoped<IMapper<ProjectItemViewModel, Project>, ProjectItemViewModelToProjectMapper>();
        }
    }
}
