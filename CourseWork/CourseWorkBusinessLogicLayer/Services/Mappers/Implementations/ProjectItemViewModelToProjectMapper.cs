using System;
using System.Linq;
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
                Id = item.Id,
                Name = item.Name,
                ImageUrl = item.ImageUrl,
                Status = item.Status,
                Rating = item.Ratings.Average(rating => rating.RatingResult),
                Description = item.Description,
                OwnerUserName = item.OwnerUserName,
                ProjectEndTime = item.FundRaisingEnd
            };
        }
    }
}
