using System;
using CourseWork.BusinessLogicLayer.Services.PhotoManagers;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class ProjectFormViewModelToProjectMapper : IMapper<ProjectFormViewModel, Project>
    {
        private readonly IPhotoManager _photoManager;

        public ProjectFormViewModelToProjectMapper(IPhotoManager photoManager)
        {
            _photoManager = photoManager;
        }

        public Project ConvertTo(ProjectFormViewModel item)
        {
            return new Project
            {
                Description = item.Description,
                FundRaisingEnd = Convert.ToDateTime(item.FundRaisingEnd),
                ImageUrl = _photoManager.LoadImage(item.ImageBase64),
                MaxPayment = item.MaxPaymentAmount,
                MinPayment = item.MinPaymentAmount,
                Name = item.Name,
            };
        }

        public ProjectFormViewModel ConvertFrom(Project item)
        {
            throw new NotImplementedException();
        }
    }
}
