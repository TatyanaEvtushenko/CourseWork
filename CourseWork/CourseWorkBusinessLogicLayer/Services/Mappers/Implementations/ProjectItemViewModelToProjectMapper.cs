using System;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class ProjectItemViewModelToProjectMapper : IMapper<ProjectItemViewModel, Project>
    {
        public Project ConvertTo(ProjectItemViewModel item)
        {
            throw new NotImplementedException();
        }

        public ProjectItemViewModel ConvertFrom(Project item)
        {
            return new ProjectItemViewModel
            {
                CreatingTime = item.CreatingTime,
                Id = item.Id,
                Name = item.Name,
                ImageUrl = item.ImageUrl,
                PaidAmount = item.PaidAmount,
                Status = item.Status
            };
        }
    }
}
