using System;
using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.PaymentManagers;
using CourseWork.BusinessLogicLayer.Services.PhotoManagers;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;
using CourseWork.BusinessLogicLayer.Services.SearchManagers;
using CourseWork.BusinessLogicLayer.ViewModels.PaymentViewModels;
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
        private readonly Repository<Tag> _tagRepository;
        private readonly Repository<FinancialPurpose> _financialPurposeRepository;
        private readonly Repository<Payment> _paymentRepository;
        private readonly Repository<UserInfo> _userInfoRepository;
        private readonly IUserManager _userManager;
        private readonly IPhotoManager _photoManager;
        private readonly IPaymentManager _paymentManager;
        private readonly IMapper<ProjectItemViewModel, Project> _projectItemMapper;
        private readonly IMapper<ProjectFormViewModel, Project> _projectFormMapper;
        private readonly IMapper<ProjectViewModel, Project> _projectMapper;
        private readonly IMapper<ProjectEditorFormViewModel, Project> _projectEditorFormMapper;
        private readonly IMapper<FinancialPurposeViewModel, FinancialPurpose> _financialPurposeMapper;
        private readonly IMapper<RatingViewModel, Rating> _ratingMapper;
        private readonly ISearchManager _searchManager;
        private readonly IMapper<PaymentFormViewModel, Payment> _paymentMapper;

        public ProjectManager(Repository<Project> projectRepository,
            IMapper<ProjectItemViewModel, Project> projectItemMapper,
            IMapper<ProjectFormViewModel, Project> projectFormMapper,
            Repository<Rating> raitingRepository,
            IMapper<ProjectViewModel, Project> projectMapper, IUserManager userManager,
            IMapper<ProjectEditorFormViewModel, Project> projectEditorFormMapper, Repository<Tag> tagRepository,
            IMapper<FinancialPurposeViewModel, FinancialPurpose> financialPurposeMapper,
            Repository<FinancialPurpose> financialPurposeRepository, IPhotoManager photoManager,
            IMapper<RatingViewModel, Rating> ratingMapper, IPaymentManager paymentManager,
            Repository<Payment> paymentRepository, Repository<UserInfo> userInfoRepository,
            IMapper<PaymentFormViewModel, Payment> paymentMapper,
            ISearchManager searchManager)
        {
            _projectRepository = projectRepository;
            _projectItemMapper = projectItemMapper;
            _projectFormMapper = projectFormMapper;
            _raitingRepository = raitingRepository;
            _projectMapper = projectMapper;
            _userManager = userManager;
            _projectEditorFormMapper = projectEditorFormMapper;
            _tagRepository = tagRepository;
            _financialPurposeMapper = financialPurposeMapper;
            _financialPurposeRepository = financialPurposeRepository;
            _photoManager = photoManager;
            _ratingMapper = ratingMapper;
            _paymentManager = paymentManager;
            _paymentRepository = paymentRepository;
            _raitingRepository = raitingRepository;
            _searchManager = searchManager;
            _userInfoRepository = userInfoRepository;
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
            var resultSuccessfully = _projectRepository.AddRange(project);
            if (resultSuccessfully)
            {
                _searchManager.AddProjectToIndex(project);
            }
            return resultSuccessfully;
        }

        public void ChangeRating(RatingViewModel ratingForm)
        {
            var ratingModel = _raitingRepository.FirstOrDefault(
                r => r.ProjectId == ratingForm.ProjectId && r.UserName == _userManager.CurrentUserName);
            if (ratingModel == null)
            {
                AddRating(ratingForm);
            }
            else
            {
                UpdateRating(ratingForm.RatingValue, ratingModel);
            }
        }

        public ProjectViewModel GetProject(string projectId)
        {
            var project = _projectRepository.Get(projectId, 
                p => p.Ratings, p => p.Comments, p => p.Payments, p => p.FinancialPurposes, p => p.News, p => p.UserInfo, p => p.Tags);
            return _projectMapper.ConvertFrom(project);
        }

        public ProjectEditorFormViewModel GetProjectEditorForm(string projectId)
        {
            var projectModel = _projectRepository.Get(projectId);
            return projectModel == null
                ? new ProjectEditorFormViewModel()
                : _projectEditorFormMapper.ConvertFrom(projectModel);
        }

        public bool UpdateProject(ProjectFormViewModel projectForm)
        {
            //var purposesForAdding = GetPurposesOfProjectForm(projectForm.FinancialPurposes, projectForm.Id).ToArray();
            //var project = GetUpdatedProject(projectForm, purposesForAdding);
            //return UpdateProject(project, purposesForAdding, projectForm.Tags);
            return false;
        }

        public IEnumerable<ProjectItemViewModel> GetUserProjects()
        {
            return GetProjects(_userManager.CurrentUserName);
        }

        public IEnumerable<ProjectItemViewModel> GetUserSubscribedProjects()
        {
            return GetSubscribedProjects(_userManager.CurrentUserName);
        }

        public IEnumerable<ProjectItemViewModel> GetProjects(string username)
        {
            return _projectRepository.GetWhere(project => project.OwnerUserName == username,
                    project => project.Subscribers, project => project.Payments)
                .Select(item => GetPreparedProjectItem(item, item.Subscribers, item.Payments));
        }

        public IEnumerable<ProjectItemViewModel> GetSubscribedProjects(string username)
        {
            return _projectRepository.GetWhere(
                    project => project.Subscribers.Where(subscriber => subscriber.UserName == username)
                        .Select(subscriber => subscriber.ProjectId).Contains(project.Id),
                    project => project.Subscribers,
                    project => project.Payments)
                .Select(project => GetPreparedProjectItem(project, project.Subscribers, project.Payments));
        }

        public IEnumerable<ProjectItemViewModel> GetLastCreatedProjects()
        {
            return _projectRepository.GetOrdered(project => project.CreatingTime, 10, true)
                .Select(project => _projectItemMapper.ConvertFrom(project));
        }

        public IEnumerable<ProjectItemViewModel> GetFinancedProjects()
        {
            return _projectRepository.GetWhere(project => project.Status == ProjectStatus.Financed).Take(10)
                .Select(project => _projectItemMapper.ConvertFrom(project));
        }

        public bool AddPayment(PaymentFormViewModel paymentForm)
        {
            UpdateAccountNumber(paymentForm.AccountNumber);
            var payment = _paymentMapper.ConvertTo(paymentForm);
            var result = _paymentRepository.AddRange(payment);
            ChangeProjectStatus(paymentForm.ProjectId);
            return result;
        }

        private bool UpdateProject(Project project, FinancialPurpose[] purposes, IEnumerable<string> tags)
        {
            var result = _projectRepository.UpdateRange(project) &&
                         UpdateFinancialPurposes(project.Id, purposes) && UpdateTagsInProject(project.Id, tags);
            if (!result)
            {
                return false;
            }
            _searchManager.SetFinancialPurposes(project.Id, purposes);
            _searchManager.SetTags(project.Id, tags.ToArray());
            return true;
        }

        private void ChangeProjectStatus(string projectId)
        {
            //var project = _projectRepository.Get(projectId);
            //var projectPayments = _paymentManager.GetProjectPayments(projectId);
            //var projectPurposes = _financialPurposeRepository.GetWhere(purpose => purpose.ProjectId == projectId);
            //ChangeProjectStatus(project, projectPayments, projectPurposes);
            //_projectRepository.UpdateRange(project);
        }

        private void UpdateAccountNumber(string accountNumber)
        {
            //var userInfo = _userManager.GetCurrentUserUserInfo();
            //userInfo.LastAccountNumber = accountNumber;
            //_userInfoRepository.UpdateRange(userInfo);
        }

        private bool IsFinancialProject(Project project)
        {
            var projectPaymentsAmount = project.Payments?.Where(p => p.Time <= project.FundRaisingEnd).Sum(p => p.PaidAmount);
            var minFinancialPurposeBudget = project.FinancialPurposes.Min(purpose => purpose.NecessaryPaymentAmount);
            return projectPaymentsAmount >= minFinancialPurposeBudget;
        }

        private bool AddTagsInProject(IEnumerable<string> tagsToAdding, string projectId)
        {
            if (tagsToAdding == null)
            {
                return true;
            }
            var tags = tagsToAdding.Select(tag => new Tag {Name = tag, ProjectId = projectId});
            return _tagRepository.AddRange(tags.ToArray());
        }

        private Project GetUpdatedProject(ProjectFormViewModel projectForm, IEnumerable<FinancialPurpose> purposes)
        {
            var project = _projectRepository.Get(projectForm.Id);
            //ConvertProjectFormToUpdatedProject(project, projectForm);
            //var projectPayments = _paymentManager.GetProjectPayments(projectForm.Id);
            //ChangeProjectStatus(project, projectPayments, purposes);
            return project;
        }

        private ProjectItemViewModel GetPreparedProjectItem(Project project, IEnumerable<ProjectSubscriber> subscribers,
            IEnumerable<Payment> payments)
        {
            var projectViewModel = _projectItemMapper.ConvertFrom(project);
            //projectViewModel.IsSubscriber =
            //    subscribers?.FirstOrDefault(subscriber => subscriber.UserName == _userManager.CurrentUserName &&
            //                                              subscriber.ProjectId == project.Id) != null;
            //projectViewModel.PaidAmount = _paymentManager.GetProjectPaidAmount(project.Id, payments);
            return projectViewModel;
        }

        private void ConvertProjectFormToUpdatedProject(Project project, ProjectFormViewModel projectForm)
        {
            project.Description = projectForm.Description;
            project.FundRaisingEnd = Convert.ToDateTime(projectForm.FundRaisingEnd);
            project.ImageUrl = _photoManager.LoadImage(projectForm.ImageBase64);
            project.MaxPayment = projectForm.MaxPaymentAmount;
            project.MinPayment = projectForm.MinPaymentAmount;
            project.Name = projectForm.Name;
        }

        private bool UpdateTagsInProject(string projectId, IEnumerable<string> newTags)
        {
            var successedRemoving =
                _tagRepository.RemoveWhere(tag => !newTags.Contains(tag.Name) && tag.ProjectId == projectId);
            var oldTags = _tagRepository.GetWhere(tag => tag.ProjectId == projectId).Select(tag => tag.Name);
            var tagsForAdding = newTags.Where(tag => !oldTags.Contains(tag));
            var successedAdding = AddTagsInProject(tagsForAdding, projectId);
            return successedRemoving && successedAdding;
        }

        private bool UpdateFinancialPurposes(string projectId, FinancialPurpose[] purposesForAdding)
        {
            return _financialPurposeRepository.RemoveWhere(purpose => purpose.ProjectId == projectId) &
                   _financialPurposeRepository.AddRange(purposesForAdding);
        }

        private void AddRating(RatingViewModel ratingViewModel)
        {
            var ratingModel = _ratingMapper.ConvertTo(ratingViewModel);
            _raitingRepository.AddRange(ratingModel);
        }

        private void UpdateRating(int rating, Rating ratingModel)
        {
            ratingModel.RatingResult = rating;
            _raitingRepository.UpdateRange(ratingModel);
        }
    }
}