using System;
using CourseWork.BusinessLogicLayer.Services.PhotoManagers;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using Microsoft.AspNetCore.Http;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class ProjectFormViewModelToProjectMapper : IMapper<ProjectFormViewModel, Project>
    {
        private readonly IPhotoManager _photoManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public ProjectFormViewModelToProjectMapper(IPhotoManager photoManager, IHttpContextAccessor contextAccessor)
        {
            _photoManager = photoManager;
            _contextAccessor = contextAccessor;
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
                OwnerUserName = _contextAccessor.HttpContext.User.Identity.Name
            };
        }

        public ProjectFormViewModel ConvertFrom(Project item)
        {
            throw new NotImplementedException();
        }
    }
}
