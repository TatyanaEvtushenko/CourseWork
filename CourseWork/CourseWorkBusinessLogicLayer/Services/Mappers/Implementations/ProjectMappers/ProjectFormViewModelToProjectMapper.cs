using System;
using CourseWork.BusinessLogicLayer.Services.FinancialPurposesManagers;
using CourseWork.BusinessLogicLayer.Services.PhotoManagers;
using CourseWork.BusinessLogicLayer.Services.TagServices;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.ProjectMappers
{
    public class ProjectFormViewModelToProjectMapper : IMapper<ProjectFormViewModel, Project>
    {
        private readonly IPhotoManager _photoManager;
        private readonly IUserManager _userManager;
        private readonly ITagService _tagService;
        private readonly IFinancialPurposeManager _financialPurposeManager;

        public ProjectFormViewModelToProjectMapper(IPhotoManager photoManager, IUserManager userManager,
            ITagService tagService, IFinancialPurposeManager financialPurposeManager)
        {
            _photoManager = photoManager;
            _userManager = userManager;
            _tagService = tagService;
            _financialPurposeManager = financialPurposeManager;
        }

        public Project ConvertTo(ProjectFormViewModel item)
        {
            var project = new Project();
            InitializeNewProject(project);
            ConvertToBaseInformation(project, item);
            InitializeCompleteFields(project, item);
            return project;
        }

        public ProjectFormViewModel ConvertFrom(Project item)
        {
            throw new NotImplementedException();
        }

        private void ConvertToBaseInformation(Project project, ProjectFormViewModel projectForm)
        {
            project.Description = projectForm.Description;
            project.FundRaisingEnd = Convert.ToDateTime(projectForm.FundRaisingEnd).ToUniversalTime();
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

        private void InitializeCompleteFields(Project model, ProjectFormViewModel viewModel)
        {
            var projectId = model.Id;
            model.Tags = _tagService.ConvertStringsToTags(viewModel.Tags, projectId);
            model.FinancialPurposes =
                _financialPurposeManager.ConvertViewModelsToPurposes(viewModel.FinancialPurposes, projectId);
        }
    }
}
