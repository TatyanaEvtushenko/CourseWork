﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly Repository<Project> _projectRepository;
        private readonly Repository<Payment> _paymentRepository;
        private readonly IUserManager _userManager;
        private readonly IPhotoManager _photoManager;
        private readonly IFinancialPurposeManager _financialPurposeManager;
        private readonly ISearchManager _searchManager;
        private readonly ITagService _tagService;
        private readonly IMapper<ProjectItemViewModel, Project> _projectItemMapper;
        private readonly IMapper<ProjectFormViewModel, Project> _projectFormMapper;
        private readonly IMapper<ProjectViewModel, Project> _projectMapper;
        private readonly IMapper<ProjectEditorFormViewModel, Project> _projectEditorFormMapper;
        private readonly IMapper<PaymentFormViewModel, Payment> _paymentMapper;

        public ProjectManager(Repository<Project> projectRepository,
            IMapper<ProjectItemViewModel, Project> projectItemMapper,
            IMapper<ProjectFormViewModel, Project> projectFormMapper,
            IMapper<ProjectViewModel, Project> projectMapper, IUserManager userManager,
            IMapper<ProjectEditorFormViewModel, Project> projectEditorFormMapper, IPhotoManager photoManager,
            Repository<Payment> paymentRepository,
            IMapper<PaymentFormViewModel, Payment> paymentMapper,
            ISearchManager searchManager, IFinancialPurposeManager financialPurposeManager, ITagService tagService)
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

        public ProjectViewModel GetProject(string projectId)
        {
            var project = _projectRepository.FirstOrDefault(p => p.Id == projectId, 
                p => p.Comments, p => p.Payments, p => p.FinancialPurposes, p => p.News, p => p.UserInfo, p => p.Tags, p => p.Ratings);
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
            return _projectRepository.GetOrdered(project => project.CreatingTime, 10, true,
                    project => project.Subscribers, project => project.Payments, p => p.Ratings)
                .Select(project => _projectItemMapper.ConvertFrom(project));
        }

        public IEnumerable<ProjectItemViewModel> GetFinancedProjects()
        {
            return _projectRepository.GetWhere(project => project.Status == ProjectStatus.Financed,
                    project => project.Subscribers, project => project.Payments, p => p.Ratings).Take(10)
                .Select(project => _projectItemMapper.ConvertFrom(project));
        }

        public bool AddPayment(PaymentFormViewModel paymentForm)
        {
            var payment = _paymentMapper.ConvertTo(paymentForm);
            if (!_paymentRepository.AddRange(payment))
            {
                return false;
            }
            UpdateProjectAfterPayment(paymentForm.ProjectId, paymentForm.AccountNumber);
            return true;
        }

        private IEnumerable<ProjectItemViewModel> GetProjectItems(Func<Project, bool> whereExpression)
        {
            var projects = _projectRepository.GetWhere(whereExpression,
                project => project.Subscribers, project => project.Payments, p => p.Ratings);
            return projects.Select(p => _projectItemMapper.ConvertFrom(p));
        }

        private void UpdateProjectAfterPayment(string projectId, string accountNumber)
        {
            var project = _projectRepository.FirstOrDefault(p => p.Id == projectId, 
                p => p.FinancialPurposes, p => p.Payments, p => p.UserInfo);
            project.UserInfo.LastAccountNumber = accountNumber;
            ChangeProjectStatus(project);
            _projectRepository.UpdateRange(project);
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