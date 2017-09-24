using System;
using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.AwardManagers;
using CourseWork.BusinessLogicLayer.Services.FinancialPurposesManagers;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.PhotoManagers;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.Services.SearchManagers;
using CourseWork.BusinessLogicLayer.Services.TagServices;
using CourseWork.BusinessLogicLayer.ViewModels.PaymentViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.ProjectManagers.Implementations
{
    public class ProjectManager : IProjectManager
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<FinancialPurpose> _financialPurposeRepository;
        private readonly IUserManager _userManager;
        private readonly IPhotoManager _photoManager;
        private readonly IFinancialPurposeManager _financialPurposeManager;
        private readonly ISearchManager _searchManager;
        private readonly ITagService _tagService;
        private readonly IAwardManager _awardManager;
        private readonly IMapper<ProjectItemViewModel, Project> _projectItemMapper;
        private readonly IMapper<ProjectFormViewModel, Project> _projectFormMapper;
        private readonly IMapper<ProjectViewModel, Project> _projectMapper;
        private readonly IMapper<ProjectEditorFormViewModel, Project> _projectEditorFormMapper;
        private readonly IMapper<PaymentFormViewModel, Payment> _paymentMapper;

        public ProjectManager(IRepository<Project> projectRepository,
            IMapper<ProjectItemViewModel, Project> projectItemMapper,
            IMapper<ProjectFormViewModel, Project> projectFormMapper,
            IMapper<ProjectViewModel, Project> projectMapper, IUserManager userManager,
            IMapper<ProjectEditorFormViewModel, Project> projectEditorFormMapper, IPhotoManager photoManager,
            IRepository<Payment> paymentRepository,
            IMapper<PaymentFormViewModel, Payment> paymentMapper,
            ISearchManager searchManager, IFinancialPurposeManager financialPurposeManager, ITagService tagService,
            IAwardManager awardManager, IRepository<Tag> tagRepository,
            IRepository<FinancialPurpose> financialPurposeRepository)
        {
            _projectRepository = projectRepository;
            _projectItemMapper = projectItemMapper;
            _projectFormMapper = projectFormMapper;
            _projectMapper = projectMapper;
            _userManager = userManager;
            _projectEditorFormMapper = projectEditorFormMapper;
            _photoManager = photoManager;
            _paymentRepository = paymentRepository;
            _searchManager = searchManager;
            _financialPurposeManager = financialPurposeManager;
            _tagService = tagService;
            _awardManager = awardManager;
            _tagRepository = tagRepository;
            _financialPurposeRepository = financialPurposeRepository;
            _paymentMapper = paymentMapper;
        }

        public double GetProjectRating(Project project)
        {
            return project.Ratings.Average(rating => rating.RatingResult);
        }

        public void ChangeProjectStatus(Project project)
        {
            if (IsFinancialProject(project))
            {
                project.Status = ProjectStatus.Financed;
            }
            else
            {
                project.Status = project.FundRaisingEnd < DateTime.UtcNow.Date ? ProjectStatus.Failed : ProjectStatus.Active;
            }
        }

        public bool AddProject(ProjectFormViewModel projectForm)
        {
            var project = _projectFormMapper.ConvertTo(projectForm);
            ChangeProjectStatus(project);
            if (!_projectRepository.AddRange(project))
            {
                return false;
            }
            ProccessProjectAfterAdding(project);
            return true;
        }

        public ProjectViewModel GetProject(string projectId)
        {
            var project = _projectRepository.FirstOrDefault(p => p.Id == projectId,
                p => p.Comments, p => p.Payments, p => p.FinancialPurposes, p => p.News, p => p.UserInfo, p => p.Tags,
                p => p.Ratings, p => p.UserInfo.Awards);
            return _projectMapper.ConvertFrom(project);
        }

        public ProjectEditorFormViewModel GetProjectEditorForm(string projectId)
        {
            var projectModel = _projectRepository.FirstOrDefault(p => p.Id == projectId,
                p => p.Payments, p => p.FinancialPurposes, p => p.Tags);
            return _projectEditorFormMapper.ConvertFrom(projectModel);
        }

        public bool UpdateProject(ProjectFormViewModel projectForm)
        {
            var project = GetUpdatedProject(projectForm);
            var result = _projectRepository.UpdateRange(project);
            if (result)
            {
                UpdateIndex(project);
            }
            return result;
        }

        public IEnumerable<ProjectItemViewModel> GetUserProjects(string userName)
        {
            return GetProjectItems(project => project.OwnerUserName == userName);
        }

        public IEnumerable<ProjectItemViewModel> GetCurrentUserProjects()
        {
            return GetProjectItems(project => project.OwnerUserName == _userManager.CurrentUserName);
        }

        public IEnumerable<ProjectItemViewModel> GetUserSubscribedProjects(string userName)
        {
            return GetProjectItems(p => p.Subscribers.FirstOrDefault(s => s.UserName == userName) != null);
        }

        public IEnumerable<ProjectItemViewModel> GetSubscribedProjects()
        {
            return GetUserSubscribedProjects(_userManager.CurrentUserName);
        }

        public IEnumerable<ProjectItemViewModel> GetLastCreatedProjects()
        {
            return _projectRepository.GetOrdered(project => project.CreatingTime, 4, true,
                    project => project.Subscribers, project => project.Payments, p => p.Ratings, p => p.FinancialPurposes)
                .Select(project => _projectItemMapper.ConvertFrom(project));
        }

        public IEnumerable<ProjectItemViewModel> GetFinancedProjects()
        {
            return _projectRepository.GetWhere(project => project.Status == ProjectStatus.Financed,
                    project => project.Subscribers, project => project.Payments, p => p.Ratings, p => p.FinancialPurposes).Take(4)
                .Select(project => _projectItemMapper.ConvertFrom(project));
        }

        public bool AddPayment(PaymentFormViewModel paymentForm)
        {
            var payment = _paymentMapper.ConvertTo(paymentForm);
            if (!_paymentRepository.AddRange(payment))
            {
                return false;
            }
            ProcessPaymentAfterAdding(payment, paymentForm.ProjectId, paymentForm.AccountNumber);
            return true;
        }

        private void ProccessProjectAfterAdding(Project project)
        {
            _searchManager.AddProjectToIndex(project);
            _awardManager.AddAwardForProjects();
        }

        private IEnumerable<ProjectItemViewModel> GetProjectItems(Func<Project, bool> whereExpression)
        {
            var projects = _projectRepository.GetWhere(whereExpression,
                project => project.Subscribers, project => project.Payments, p => p.Ratings, p => p.FinancialPurposes);
            return projects.Select(p => _projectItemMapper.ConvertFrom(p));
        }

        private void ProcessPaymentAfterAdding(Payment payment, string projectId, string accountNumber)
        {
            _awardManager.AddAwardForPayments(payment);
            var project = UpdateProjectAfterPayment(projectId, accountNumber);
            _awardManager.AddAwardForReceivedPayments(project);
        }

        private Project UpdateProjectAfterPayment(string projectId, string accountNumber)
        {
            var project = _projectRepository.FirstOrDefault(p => p.Id == projectId,
                p => p.FinancialPurposes, p => p.Payments, p => p.UserInfo);
            project.UserInfo.LastAccountNumber = accountNumber;
            ChangeProjectStatus(project);
            _projectRepository.UpdateRange(project);
            return project;
        }

        private bool IsFinancialProject(Project project)
        {
            var projectPaymentsAmount = project.Payments?.Where(p => p.Time <= project.FundRaisingEnd).Sum(p => p.PaidAmount);
            var minFinancialPurposeBudget = project.FinancialPurposes.Min(purpose => purpose.NecessaryPaymentAmount);
            return projectPaymentsAmount >= minFinancialPurposeBudget;
        }

        private Project GetUpdatedProject(ProjectFormViewModel projectForm)
        {
            var project = _projectRepository.FirstOrDefault(p => p.Id == projectForm.Id, p => p.Payments);
            UpdateBaseProjectInfo(project, projectForm);
            UpdateCompleteProjectInfo(project, projectForm);
            ChangeProjectStatus(project);
            return project;
        }

        private void UpdateBaseProjectInfo(Project project, ProjectFormViewModel projectForm)
        {
            project.Description = projectForm.Description;
            project.FundRaisingEnd = Convert.ToDateTime(projectForm.FundRaisingEnd);
            project.Name = projectForm.Name;
            UpdateProjectDesignInformation(project, projectForm);
            UpdateProjectPaymentInfo(project, projectForm);
        }

        private void UpdateProjectDesignInformation(Project project, ProjectFormViewModel projectForm)
        {
            project.ImageUrl = _photoManager.LoadImage(projectForm.ImageBase64);
            project.Color = projectForm.Color;
        }

        private void UpdateProjectPaymentInfo(Project project, ProjectFormViewModel projectForm)
        {
            project.MaxPayment = projectForm.MaxPaymentAmount;
            project.MinPayment = projectForm.MinPaymentAmount;
            project.AccountNumber = projectForm.AccountNumber;
        }

        private void UpdateCompleteProjectInfo(Project project, ProjectFormViewModel projectForm)
        {
            UpdateFinancialPurposes(project, projectForm);
            UpdateTagsInProject(project, projectForm);
        }

        private void UpdateIndex(Project project)
        {
            _searchManager.SetFinancialPurposes(project.Id, project.FinancialPurposes.ToArray());
            _searchManager.SetTags(project.Id, _tagService.GetProjectTags(project).ToArray());
        }

        private void UpdateTagsInProject(Project project, ProjectFormViewModel projectForm)
        {
            _tagRepository.RemoveWhere(tag => !projectForm.Tags.Contains(tag.Name) && tag.ProjectId == project.Id);
            var oldTags = _tagRepository.GetWhere(tag => tag.ProjectId == project.Id).Select(tag => tag.Name);
            var tagsForAdding = projectForm.Tags.Where(tag => !oldTags.Contains(tag));
            project.Tags = tagsForAdding.Select(t => GetTagForAdding(t, project)).ToList();
        }

        private void UpdateFinancialPurposes(Project project, ProjectFormViewModel projectForm)
        {
            project.FinancialPurposes =
                _financialPurposeManager.ConvertViewModelsToPurposes(projectForm.FinancialPurposes, project.Id).ToList();
            _financialPurposeRepository.RemoveWhere(purpose => purpose.ProjectId == project.Id);
        }

        private Tag GetTagForAdding(string name, Project project)
        {
            return new Tag {Name = name, ProjectId = project.Id};
        }
    }
}