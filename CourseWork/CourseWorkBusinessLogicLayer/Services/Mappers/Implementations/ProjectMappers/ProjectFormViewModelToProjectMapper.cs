using System;
using System.Linq;
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
            InitializeNewProject(project, item);
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
            project.Name = projectForm.Name;
            project.Description = projectForm.Description;
            project.FundRaisingEnd = DateTime.ParseExact(projectForm.FundRaisingEnd, "dd/MM/yyyy", null).ToUniversalTime();
            ConvertToDesignInformation(project, projectForm);
            ConvertToPaymentInformation(project, projectForm);
        }

        private void ConvertToDesignInformation(Project project, ProjectFormViewModel projectForm)
        {
            project.ImageUrl = _photoManager.LoadImage(projectForm.ImageBase64);
            project.Color = projectForm.Color;
        }

        private void ConvertToPaymentInformation(Project project, ProjectFormViewModel projectForm)
        {
            project.MaxPayment = projectForm.MaxPaymentAmount;
            project.MinPayment = projectForm.MinPaymentAmount;
            project.AccountNumber = projectForm.AccountNumber;
        }

        private void InitializeNewProject(Project project, ProjectFormViewModel projectForm)
        {
            project.CreatingTime = DateTime.UtcNow;
            project.Id = projectForm.Id ?? Guid.NewGuid().ToString();
            project.OwnerUserName = _userManager.CurrentUserName;
        }

        private void InitializeCompleteFields(Project model, ProjectFormViewModel viewModel)
        {
            var projectId = model.Id;
            model.Tags = _tagService.ConvertStringsToTags(viewModel.Tags, projectId).ToList();
            model.FinancialPurposes = 
                _financialPurposeManager.ConvertViewModelsToPurposes(viewModel.FinancialPurposes, projectId).ToList();
        }
    }
}
