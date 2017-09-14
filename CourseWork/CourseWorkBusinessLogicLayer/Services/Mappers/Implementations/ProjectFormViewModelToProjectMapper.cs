using System;
using CourseWork.BusinessLogicLayer.Services.PhotoManagers;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class ProjectFormViewModelToProjectMapper : IMapper<ProjectFormViewModel, Project>
    {
        private readonly IPhotoManager _photoManager;
        private readonly IUserManager _userManager;

        public ProjectFormViewModelToProjectMapper(IPhotoManager photoManager, IUserManager userManager)
        {
            _photoManager = photoManager;
            _userManager = userManager;
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
                CreatingTime = DateTime.Today,
                Id = Guid.NewGuid().ToString(),
                Status = ProjectStatus.Active,
                OwnerUserName = _userManager.CurrentUserName
            };
        }

        public ProjectFormViewModel ConvertFrom(Project item)
        {
            throw new NotImplementedException();
        }
    }
}
