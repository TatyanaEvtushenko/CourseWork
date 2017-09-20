using System;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.PhotoManagers;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.ProjectMappers
{
    public class ProjectFormViewModelToProjectMapper : IMapper<ProjectFormViewModel, Project>
    {
        private readonly IPhotoManager _photoManager;
        private readonly IUserManager _userManager;
        private readonly IMapper<FinancialPurposeViewModel, FinancialPurpose> _financialPurposeMapper;

        public ProjectFormViewModelToProjectMapper(IPhotoManager photoManager, IUserManager userManager,
            IMapper<FinancialPurposeViewModel, FinancialPurpose> financialPurposeMapper)
        {
            _photoManager = photoManager;
            _userManager = userManager;
            _financialPurposeMapper = financialPurposeMapper;
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
            model.FinancialPurposes = viewModel.FinancialPurposes.Select(p => GetNewFinancialPurpose(p, projectId)); ;
            model.Tags = viewModel.Tags.Select(t => GetNewTag(t, projectId));
        }

        private Tag GetNewTag(string name, string projectId)
        {
            return new Tag {Name = name, ProjectId = projectId};
        }

        private FinancialPurpose GetNewFinancialPurpose(FinancialPurposeViewModel purpose, string projectId)
        {
            var purposeToAdding = _financialPurposeMapper.ConvertTo(purpose);
            purposeToAdding.ProjectId = projectId;
            return purposeToAdding;
        }
    }
}
