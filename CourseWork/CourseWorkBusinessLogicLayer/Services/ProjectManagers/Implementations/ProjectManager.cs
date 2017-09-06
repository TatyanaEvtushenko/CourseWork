using System;
using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.FinancialPurposeManagers;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.PaymentManagers;
using CourseWork.BusinessLogicLayer.Services.TagServices;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Microsoft.AspNetCore.Http;

namespace CourseWork.BusinessLogicLayer.Services.ProjectManagers.Implementations
{
    public class ProjectManager : IProjectManager
    {
        private readonly Repository<Project> _projectRepository;
        private readonly ITagService _tagService;
        private readonly IFinancialPurposeManager _financialPurposeManager;
        private readonly IPaymentManager _paymentManager;
        private readonly IMapper<ProjectItemViewModel, Project> _projectMapper;
        private readonly IMapper<ProjectFormViewModel, Project> _projectFormMapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public ProjectManager(Repository<Project> projectRepository,
            IMapper<ProjectItemViewModel, Project> projectMapper,
            IMapper<ProjectFormViewModel, Project> projectFormMapper, ITagService tagService,
            IFinancialPurposeManager financialPurposeManager,
            IHttpContextAccessor contextAccessor, IPaymentManager paymentManager)
        {
            _projectRepository = projectRepository;
            _projectMapper = projectMapper;
            _projectFormMapper = projectFormMapper;
            _tagService = tagService;
            _financialPurposeManager = financialPurposeManager;
            _contextAccessor = contextAccessor;
            _paymentManager = paymentManager;
        }

        public bool AddProject(ProjectFormViewModel projectForm)
        {
            var project = GetPreparedProject(projectForm);
            return _projectRepository.AddRange(project) && _tagService.AddTagsInProject(projectForm.Tags, project.Id) &&
                   _financialPurposeManager.AddFinancialPurposes(projectForm.FinancialPurposes, project.Id);
        }

        public void UpdateExistedProjects()
        {
            var activeProjects = _projectRepository.GetWhere(project => project.Status == ProjectStatus.Active);
            activeProjects.ForEach(project => ChangeProjectStatus(ref project));
            _projectRepository.UpdateRange(activeProjects.ToArray());
        }

        public IEnumerable<ProjectItemViewModel> GetLastCreatedProjects()
        {
            return _projectRepository.GetAll().OrderByDescending(project => project.CreatingTime).Take(10)
                .Select(project => _projectMapper.ConvertFrom(project));
        }

        public IEnumerable<ProjectItemViewModel> GetFinancedProjects()
        {
            return _projectRepository.GetWhere(project => project.Status == ProjectStatus.Financed)
                .Select(project => _projectMapper.ConvertFrom(project));
        }

        private void ChangeProjectStatus(ref Project project)
        {
            if (IsFinancialProject(project))
            {
                project.Status = ProjectStatus.Financed;
            }
            else
            {
                if (project.FundRaisingEnd > DateTime.Today)
                {
                    project.Status = ProjectStatus.Failed;
                }
            }
        }

        private bool IsFinancialProject(Project project)
        {
            return project.FundRaisingEnd <= _paymentManager.GetTimeLastPayment(project.Id) 
                && project.PaidAmount >= _financialPurposeManager.GetMinFinancialPurposeBudget(project.Id);
        }

        private Project GetPreparedProject(ProjectFormViewModel projectForm)
        {
            var project = _projectFormMapper.ConvertTo(projectForm);
            project.CreatingTime = DateTime.Today;
            project.OwnerId = _contextAccessor.HttpContext.User.Identity.Name;
            project.Status = ProjectStatus.Active;
            project.Id = _projectRepository.GetNewId();
            project.PaidAmount = 0;
            return project;
        }
    }
}
