using System;
using CourseWork.BusinessLogicLayer.Services.PhotoManagers;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
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
            var project = new Project();
            ConvertToBaseInformation(project, item);
            InitializeNewProject(project);
            return project;
        }

        public ProjectFormViewModel ConvertFrom(Project item)
        {
            throw new NotImplementedException();
        }

        private void ConvertToBaseInformation(Project project, ProjectFormViewModel projectForm)
        {
            project.Description = projectForm.Description;
            project.FundRaisingEnd = Convert.ToDateTime(projectForm.FundRaisingEnd);
            project.ImageUrl = _photoManager.LoadImage(projectForm.ImageBase64);
            project.MaxPayment = projectForm.MaxPaymentAmount;
            project.MinPayment = projectForm.MinPaymentAmount;
            project.Name = projectForm.Name;
        }

        private void InitializeNewProject(Project project)
        {
            project.CreatingTime = DateTime.UtcNow;
            project.Id = Guid.NewGuid().ToString();
            project.OwnerUserName = _userManager.CurrentUserName;
        }
    }
}
