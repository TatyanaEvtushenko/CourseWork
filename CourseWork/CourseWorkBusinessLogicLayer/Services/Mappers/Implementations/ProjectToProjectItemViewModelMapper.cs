using System;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    internal class ProjectToProjectItemViewModelMapper : IMapper<Project, ProjectItemViewModel>
    {
        public ProjectItemViewModel ConvertTo(Project item)
        {
            return new ProjectItemViewModel
            {
                CreatingTime = item.CreatingTime,
                Id = item.Id,
                Name = item.Name,
                PaidAmount = item.PaidAmount
            };
        }

        public Project ConvertFrom(ProjectItemViewModel item)
        {
            throw new NotImplementedException();
        }
    }
}
