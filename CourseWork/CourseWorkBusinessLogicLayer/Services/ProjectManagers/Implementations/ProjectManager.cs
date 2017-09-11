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
        private readonly Repository<Raiting> _raitingRepository;
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
            IHttpContextAccessor contextAccessor, IPaymentManager paymentManager, Repository<Raiting> raitingRepository)
        {
            _projectRepository = projectRepository;
            _projectMapper = projectMapper;
            _projectFormMapper = projectFormMapper;
            _tagService = tagService;
            _financialPurposeManager = financialPurposeManager;
            _contextAccessor = contextAccessor;
            _paymentManager = paymentManager;
            _raitingRepository = raitingRepository;
        }

        public bool AddProject(ProjectFormViewModel projectForm)
        {
            var project = GetPreparedProject(projectForm);
            return _projectRepository.AddRange(project) & _tagService.AddTagsInProject(projectForm.Tags, project.Id) &
                   _financialPurposeManager.AddFinancialPurposes(projectForm.FinancialPurposes, project.Id);
        }

        public IEnumerable<ProjectItemViewModel> GetUserProjects()
        {
            var userName = _contextAccessor.HttpContext.User.Identity.Name;
            return _projectRepository.GetWhere(project => project.OwnerUserName == userName)
                .Select(project => _projectMapper.ConvertFrom(project));
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

        public string GetProjectName(string projectId)
        {
            return _projectRepository.Get(projectId).Name;
        }

        public void UpdateExistedProjects()
        {
            var projects = _projectRepository.GetAll();
            UpdateProjectRaiting(projects);
            UpdateProjectStatus(projects);
            _projectRepository.UpdateRange(projects.ToArray());
        }

        private void UpdateProjectRaiting(IEnumerable<Project> projects)
        {
            var raitings = _raitingRepository.GetAll();
            foreach (var project in projects)
            {
                var projectRaitings = raitings.Where(raiting => raiting.ProjectId == project.Id);
                project.Raiting = !projectRaitings.Any() ? 0 : projectRaitings.Average(raiting => raiting.RaitingResult);
            }
        }

        private void UpdateProjectStatus(IEnumerable<Project> projects)
        {
            var activeProjects = projects.Where(project => project.Status == ProjectStatus.Active);
            foreach (var project in activeProjects)
            {
                ChangeProjectStatus(project);
            }
        }

        private void ChangeProjectStatus(Project project)
        {
            if (IsFinancialProject(project))
            {
                project.Status = ProjectStatus.Financed;
            }
            else
            {
                if (project.FundRaisingEnd < DateTime.Today)
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
            project.OwnerUserName = _contextAccessor.HttpContext.User.Identity.Name;
            project.Status = ProjectStatus.Active;
            project.Id = _projectRepository.GetNewId();
            return project;
        }
    }
}
