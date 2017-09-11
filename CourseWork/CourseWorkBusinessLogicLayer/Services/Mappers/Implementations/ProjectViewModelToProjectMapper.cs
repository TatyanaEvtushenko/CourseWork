using System;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class ProjectViewModelToProjectMapper : IMapper<ProjectViewModel, Project>
    {
        public Project ConvertTo(ProjectViewModel item)
        {
            throw new NotImplementedException();
        }

        public ProjectViewModel ConvertFrom(Project item)
        {
            return new ProjectViewModel
            {
                Description = item.Description,
                FundRaisingEnd = item.FundRaisingEnd,
                MaxPaymentAmount = item.MaxPayment,
                MinPaymentAmount = item.MinPayment,
                Id = item.Id,
                ImageUrl = item.ImageUrl,
                Name = item.Name,
            };
        }
    }
}
