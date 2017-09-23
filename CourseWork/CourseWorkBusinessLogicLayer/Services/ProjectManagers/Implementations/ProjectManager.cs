using System;
using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Options;
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
using Microsoft.Extensions.Options;

namespace CourseWork.BusinessLogicLayer.Services.ProjectManagers.Implementations
{
    public class ProjectManager : IProjectManager
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<Payment> _paymentRepository;
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
        private readonly HomePageOptions _options;

        public ProjectManager(IRepository<Project> projectRepository,
            IMapper<ProjectItemViewModel, Project> projectItemMapper,
            IMapper<ProjectFormViewModel, Project> projectFormMapper,
            IMapper<ProjectViewModel, Project> projectMapper, IUserManager userManager,
            IMapper<ProjectEditorFormViewModel, Project> projectEditorFormMapper, IPhotoManager photoManager,
            IRepository<Payment> paymentRepository,
            IMapper<PaymentFormViewModel, Payment> paymentMapper,
            ISearchManager searchManager, IFinancialPurposeManager financialPurposeManager, ITagService tagService,
            IAwardManager awardManager, IOptions<HomePageOptions> options)
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
            _options = options.Value;
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

        public bool AddProject(ProjectFormViewModel projectForm, string awardName)
        {
            var project = _projectFormMapper.ConvertTo(projectForm);
            ChangeProjectStatus(project);
            if (!_projectRepository.AddRange(project))
            {
                return false;
            }
            ProccessProjectAfterAdding(project, awardName);
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
            return _projectRepository.GetOrdered(project => project.CreatingTime, _options.LastCreatedProjectsCount, true,
                    project => project.Subscribers, project => project.Payments, p => p.Ratings, p => p.FinancialPurposes)
                .Select(project => _projectItemMapper.ConvertFrom(project));
        }

        public IEnumerable<ProjectItemViewModel> GetFinancedProjects()
        {
            return _projectRepository.GetWhere(project => project.Status == ProjectStatus.Financed,
                    project => project.Subscribers, project => project.Payments, p => p.Ratings, p => p.FinancialPurposes).Take(_options.FinancedProjectsCount)
                .Select(project => _projectItemMapper.ConvertFrom(project));
        }

        public bool AddPayment(PaymentFormViewModel paymentForm, string awardNamePayment, string awardNameReceived)
        {
            var payment = _paymentMapper.ConvertTo(paymentForm);
            if (!_paymentRepository.AddRange(payment))
            {
                return false;
            }
            ProcessPaymentAfterAdding(payment, paymentForm.ProjectId, paymentForm.AccountNumber, awardNamePayment, awardNameReceived);
            return true;
        }

        private void ProccessProjectAfterAdding(Project project, string awardName)
        {
            _searchManager.AddProjectToIndex(project);
            _awardManager.AddAwardForProjects(awardName);
        }

        private IEnumerable<ProjectItemViewModel> GetProjectItems(Func<Project, bool> whereExpression)
        {
            var projects = _projectRepository.GetWhere(whereExpression,
                project => project.Subscribers, project => project.Payments, p => p.Ratings, p => p.FinancialPurposes );
            return projects.Select(p => _projectItemMapper.ConvertFrom(p));
        }

        private void ProcessPaymentAfterAdding(Payment payment, string projectId, string accountNumber, string awardNamePayment, string awardNameReceived)
        {
            _awardManager.AddAwardForPayments(payment, awardNamePayment);
            var project = UpdateProjectAfterPayment(projectId, accountNumber);
            _awardManager.AddAwardForReceivedPayments(project, awardNameReceived);
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
            project.ImageUrl = _photoManager.LoadImage(projectForm.ImageBase64);
            project.MaxPayment = projectForm.MaxPaymentAmount;
            project.MinPayment = projectForm.MinPaymentAmount;
            project.Name = projectForm.Name;
        }

        private void UpdateCompleteProjectInfo(Project project, ProjectFormViewModel projectForm)
        {
            project.Tags = _tagService.ConvertStringsToTags(projectForm.Tags, project.Id);
            project.FinancialPurposes =
                _financialPurposeManager.ConvertViewModelsToPurposes(projectForm.FinancialPurposes, project.Id);
        }

        private void UpdateIndex(Project project)
        {
            _searchManager.SetFinancialPurposes(project.Id, project.FinancialPurposes.ToArray());
            _searchManager.SetTags(project.Id, _tagService.GetProjectTags(project).ToArray());
        }

        //private bool UpdateTagsInProject(string projectId, IEnumerable<string> newTags)
        //{
        //    var successedRemoving =
        //        _tagRepository.RemoveWhere(tag => !newTags.Contains(tag.Name) && tag.ProjectId == projectId);
        //    var oldTags = _tagRepository.GetWhere(tag => tag.ProjectId == projectId).Select(tag => tag.Name);
        //    var tagsForAdding = newTags.Where(tag => !oldTags.Contains(tag));
        //    var successedAdding = AddTagsInProject(tagsForAdding, projectId);
        //    return successedRemoving && successedAdding;
        //}

        //private bool UpdateFinancialPurposes(string projectId, FinancialPurpose[] purposesForAdding)
        //{
        //    return _financialPurposeRepository.RemoveWhere(purpose => purpose.ProjectId == projectId) &
        //           _financialPurposeRepository.AddRange(purposesForAdding);
        //}
    }
}