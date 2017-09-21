using System;
using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.FinancialPurposesManagers;
using CourseWork.BusinessLogicLayer.Services.PaymentManagers;
using CourseWork.BusinessLogicLayer.Services.ProjectSubscriberManagers;
using CourseWork.BusinessLogicLayer.Services.RatingManagers;
using CourseWork.BusinessLogicLayer.Services.TagServices;
using CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.NewsViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using CourseWork.DataLayer.Repositories.Implementations;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.ProjectMappers
{
    public class ProjectViewModelToProjectMapper : IMapper<ProjectViewModel, Project>
    {
        private readonly Repository<UserInfo> _userInfoRepository;
        private readonly IMapper<NewsViewModel, News> _newsMapper;
        private readonly IMapper<CommentViewModel, Comment> _commentMapper;
        private readonly IMapper<UserSmallViewModel, UserInfo> _userInfoMapper;
        private readonly ITagService _tagService;
        private readonly IPaymentManager _paymentManager;
        private readonly IFinancialPurposeManager _financialPurposeManager;
        private readonly IProjectSubscriberManager _projectSubscriberManager;
        private readonly IRatingManager _ratingManager;

        public ProjectViewModelToProjectMapper(
            IMapper<CommentViewModel, Comment> commentMapper, IMapper<NewsViewModel, News> newsMapper,
            ITagService tagService, IPaymentManager paymentManager,
            IFinancialPurposeManager financialPurposeManager, Repository<UserInfo> userInfoRepository,
            IMapper<UserSmallViewModel, UserInfo> userInfoMapper, IProjectSubscriberManager projectSubscriberManager,
            IRatingManager ratingManager)
        {
            _commentMapper = commentMapper;
            _newsMapper = newsMapper;
            _tagService = tagService;
            _paymentManager = paymentManager;
            _financialPurposeManager = financialPurposeManager;
            _userInfoRepository = userInfoRepository;
            _userInfoMapper = userInfoMapper;
            _projectSubscriberManager = projectSubscriberManager;
            _ratingManager = ratingManager;
        }

        public Project ConvertTo(ProjectViewModel item)
        {
            throw new NotImplementedException();
        }

        public ProjectViewModel ConvertFrom(Project item)
        {
            var project = new ProjectViewModel();
            ConvertFromBaseInformation(project, item);
            ConvertFromCurrentUser(project, item);
            ConvertFromCompleteObjects(project, item);
            return project;
        }

        private void ConvertFromBaseInformation(ProjectViewModel viewModel, Project model)
        {
            viewModel.Id = model.Id;
            viewModel.Name = model.Name;
            viewModel.Description = model.Description;
            viewModel.ImageUrl = model.ImageUrl;
            viewModel.Status = model.Status;
            viewModel.FundRaisingEnd = model.FundRaisingEnd;
        }

        private void ConvertFromCurrentUser(ProjectViewModel viewModel, Project model)
        {
            viewModel.IsSubscriber = _projectSubscriberManager.IsSubscriber(model.Id);
        }

        private void ConvertFromCompleteObjects(ProjectViewModel viewModel, Project model)
        {
            viewModel.Rating = _ratingManager.GetProjectRatings(model);
            viewModel.Owner = _userInfoMapper.ConvertFrom(model.UserInfo);
            viewModel.PaidAmount = _paymentManager.GetProjectPaidAmount(model);
            viewModel.CountOfPayments = model.Payments.Count();
            viewModel.Tags = _tagService.GetProjectTags(model);
            viewModel.News = model.News.Where(n => n.Type == NewsType.News).Select(n => _newsMapper.ConvertFrom(n));
            viewModel.FinancialPurposes = _financialPurposeManager.GetProjectFinancialPurposes(model, viewModel.PaidAmount);
            ConvertFromComments(viewModel, model);
        }

        private void ConvertFromComments(ProjectViewModel viewModel, Project project)
        {
            var commentatorUserNames = project.Comments.Select(c => c.UserName);
            var commentatorInfos = _userInfoRepository.GetWhere(i => commentatorUserNames.Contains(i.UserName));
            viewModel.Comments = project.Comments.Select(c => GetComment(commentatorInfos, c));
        }

        private CommentViewModel GetComment(IEnumerable<UserInfo> commentatorsInfo, Comment commentModel)
        {
            commentModel.UserInfo = commentatorsInfo.FirstOrDefault(i => i.UserName == commentModel.UserName);
            return _commentMapper.ConvertFrom(commentModel);
        }
    }
}