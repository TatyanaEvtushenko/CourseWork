using System;
using CourseWork.BusinessLogicLayer.Services.PhotoManagers;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class ProjectFormViewModelToProjectMapper : IMapper<ProjectFormViewModel, Project>
    {
        private readonly IPhotoManager _photoManager;
        private readonly IUserManager _userManager;
        private readonly Repository<Project> _projectRepository;
        
        public ProjectFormViewModelToProjectMapper(IPhotoManager photoManager, IUserManager userManager,
            Repository<Project> projectRepository)
        {
            _photoManager = photoManager;
            _userManager = userManager;
            _projectRepository = projectRepository;
        }

        public Project ConvertTo(ProjectFormViewModel item)
        {
            var project = new Project();
            ConvertToBaseInformation(project, item);
            if (item.Id == null)
            {
                InitializeNewProject(project);
            }
            else
            {
                project = InitializeExistedProject(project, item.Id);
            }
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
            project.ImageUrl = GetCorrectImageUrl(projectForm.ImageBase64);
            project.MaxPayment = projectForm.MaxPaymentAmount;
            project.MinPayment = projectForm.MinPaymentAmount;
            project.Name = projectForm.Name;
        }

        private string GetCorrectImageUrl(string image)
        {
            return Uri.IsWellFormedUriString(image, UriKind.Absolute) ? image : _photoManager.LoadImage(image);
        }

        private void InitializeNewProject(Project project)
        {
            project.CreatingTime = DateTime.Today.ToUniversalTime();
            project.Id = Guid.NewGuid().ToString();
            project.Status = ProjectStatus.Active;
            project.OwnerUserName = _userManager.CurrentUserName;
        }

        private Project InitializeExistedProject(Project projectModel, string projectId)
        {
            var model = _projectRepository.FirstOrDefault(project => project.Id == projectId);
            if (model == null)
            {
                return null;
            }
            ChangeExistedProjectFields(projectModel, model);
            return projectModel;
        }

        private void ChangeExistedProjectFields(Project projectModel, Project model)
        {
            projectModel.Id = model.Id;
            projectModel.OwnerUserName = model.OwnerUserName;
            projectModel.CreatingTime = model.CreatingTime;
            projectModel.PaidAmount = projectModel.PaidAmount;
        }
    }
}
