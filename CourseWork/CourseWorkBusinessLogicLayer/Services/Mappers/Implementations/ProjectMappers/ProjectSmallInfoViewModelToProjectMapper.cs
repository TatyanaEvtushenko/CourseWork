using System;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.ProjectMappers
{
    public class ProjectSmallInfoViewModelToProjectMapper : IMapper<ProjectSmallInfoViewModel, Project>
    {
        public Project ConvertTo(ProjectSmallInfoViewModel item)
        {
            throw new NotImplementedException();
        }

        public ProjectSmallInfoViewModel ConvertFrom(Project item)
        {
            return new ProjectSmallInfoViewModel
            {
                Id = item.Id,
                Name = item.Name
            };
        }
    }
}
