using System;
using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.FinancialPurposeManagers;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.PaymentManagers;
using CourseWork.BusinessLogicLayer.Services.TagServices;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.ProjectManagers.Implementations
{
    public class ProjectManager : IProjectManager
    {
        private readonly Repository<Project> _projectRepository;
        private readonly Repository<Rating> _raitingRepository;
        private readonly Repository<FinancialPurpose> _financialPurposeRepository;
        private readonly ITagService _tagService;
        private readonly IFinancialPurposeManager _financialPurposeManager;
        private readonly IPaymentManager _paymentManager;
        private readonly IUserManager _userManager;
        private readonly IMapper<ProjectItemViewModel, Project> _projectItemMapper;
        private readonly IMapper<ProjectFormViewModel, Project> _projectFormMapper;
        private readonly IMapper<ProjectViewModel, Project> _projectMapper;
        private readonly IMapper<ProjectEditorFormViewModel, Project> _projectEditorFormMapper;

        public ProjectManager(Repository<Project> projectRepository,
            IMapper<ProjectItemViewModel, Project> projectItemMapper,
            IMapper<ProjectFormViewModel, Project> projectFormMapper, ITagService tagService,
            IFinancialPurposeManager financialPurposeManager, IPaymentManager paymentManager,
            Repository<Rating> raitingRepository,
            IMapper<ProjectViewModel, Project> projectMapper, IUserManager userManager,
            IMapper<ProjectEditorFormViewModel, Project> projectEditorFormMapper, Repository<FinancialPurpose> financialPurposeRepository)
        {
            _projectRepository = projectRepository;
            _projectItemMapper = projectItemMapper;
            _projectFormMapper = projectFormMapper;
            _tagService = tagService;
            _financialPurposeManager = financialPurposeManager;
            _paymentManager = paymentManager;
            _raitingRepository = raitingRepository;
            _projectMapper = projectMapper;
            _userManager = userManager;
            _projectEditorFormMapper = projectEditorFormMapper;
            _financialPurposeRepository = financialPurposeRepository;
        }

        public bool AddProject(ProjectFormViewModel projectForm)
        {
            var project = _projectFormMapper.ConvertTo(projectForm);
            return _projectRepository.AddRange(project) & _tagService.AddTagsInProject(projectForm.Tags, project.Id) &
                   _financialPurposeManager.AddFinancialPurposes(projectForm.FinancialPurposes, project.Id);
        }

        public void ChangeRating(RatingViewModel ratingForm)
        {
            var ratingModel = _raitingRepository.FirstOrDefault(
                    rating => rating.ProjectId == ratingForm.ProjectId && rating.UserName == _userManager.CurrentUserName);
            if (ratingModel == null)
            {
                AddRating(ratingForm, _userManager.CurrentUserName);
            }
            else
            {
                UpdateRating(ratingForm.RatingValue, ratingModel);
            }
        }

        public ProjectViewModel GetProject(string projectId)
        {
            var project = _projectRepository.Get(projectId);
            return _projectMapper.ConvertFrom(project);
        }

        public ProjectEditorFormViewModel GetProjectEditorForm(string projectId)
        {
            var projectModel = _projectRepository.FirstOrDefault(project => project.Id == projectId);
            return projectModel == null ? null : _projectEditorFormMapper.ConvertFrom(projectModel);
        }

        public bool UpdateProject(ProjectFormViewModel projectForm)
        {
            var project = _projectFormMapper.ConvertTo(projectForm);
            ChangeProjectStatus(project);
            _tagService.AddTagsInProject(projectForm.Tags, project.Id);
            //var oldFinancialPurposeIds = _financialPurposeRepository
            //    .GetWhere(purpose => purpose.ProjectId == project.Id).Select(purpose => purpose.Id);
            //var purposesForRemovingIds = oldFinancialPurposeIds.Where(oldPurpose =>  projectForm.FinancialPurposes)
            return _projectRepository.UpdateRange(project);
        }

        public IEnumerable<ProjectItemViewModel> GetUserProjects()
        {
            return _projectRepository.GetWhere(project => project.OwnerUserName == _userManager.CurrentUserName)
                .Select(project => _projectItemMapper.ConvertFrom(project));
        }

        public IEnumerable<ProjectItemViewModel> GetLastCreatedProjects()
        {
            return _projectRepository.GetAll().OrderByDescending(project => project.CreatingTime).Take(10)
                .Select(project => _projectItemMapper.ConvertFrom(project));
        }

        public IEnumerable<ProjectItemViewModel> GetFinancedProjects()
        {
            return _projectRepository.GetWhere(project => project.Status == ProjectStatus.Financed)
                .Select(project => _projectItemMapper.ConvertFrom(project));
        }

        public void UpdateExistedProjects()
        {
            var projects = _projectRepository.GetAll();
            UpdateProjectRaiting(projects);
            UpdateProjectStatus(projects);
            _projectRepository.UpdateRange(projects.ToArray());
        }

        private void UpdateProjectStatus(IEnumerable<Project> projects)
        {
            var activeProjects = projects.Where(project => project.Status == ProjectStatus.Active);
            foreach (var project in activeProjects)
            {
                ChangeProjectStatus(project);
            }
        }

        private void AddRating(RatingViewModel rating, string userName)
        {
            _raitingRepository.AddRange(new Rating
            {
                ProjectId = rating.ProjectId,
                RaitingResult = rating.RatingValue,
                UserName = userName
            });
        }

        private void UpdateRating(int rating, Rating ratingModel)
        {
            ratingModel.RaitingResult = rating;
            _raitingRepository.UpdateRange(ratingModel);
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

        private void ChangeProjectStatus(Project project)
        {
            if (IsFinancialProject(project))
            {
                project.Status = ProjectStatus.Financed;
            }
            else
            {
                project.Status = project.FundRaisingEnd < DateTime.Today ? ProjectStatus.Failed : ProjectStatus.Active;
            }
        }

        private bool IsFinancialProject(Project project)
        {
            return project.FundRaisingEnd <= _paymentManager.GetTimeLastPayment(project.Id) 
                && project.PaidAmount >= _financialPurposeManager.GetMinFinancialPurposeBudget(project.Id);
        }
    }
}

