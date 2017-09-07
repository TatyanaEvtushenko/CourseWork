using System;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class ProjectFormViewModelToProjectMapper : IMapper<ProjectFormViewModel, Project>
    {
        public Project ConvertTo(ProjectFormViewModel item)
        {
            return new Project
            {
                //Description = item.Description,
                //FundRaisingEnd = item.FundRaisingEnd,
                //ImageUrl = item.ImageUrl,
                //MaxPayment = item.MaxPaymentAmount,
                //MinPayment = item.MinPaymentAmount,
                //Name = item.Name,
            };
        }

        public ProjectFormViewModel ConvertFrom(Project item)
        {
            throw new NotImplementedException();
        }
    }
}
