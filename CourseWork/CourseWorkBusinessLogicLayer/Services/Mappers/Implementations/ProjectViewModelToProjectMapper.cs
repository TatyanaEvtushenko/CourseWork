using System;
using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.FinancialPurposesManagers;
using CourseWork.BusinessLogicLayer.Services.PaymentManagers;
using CourseWork.BusinessLogicLayer.Services.TagServices;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.NewsViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class ProjectViewModelToProjectMapper : IMapper<ProjectViewModel, Project>
    {
        private readonly Repository<Payment> _paymentRepository;
        private readonly Repository<Rating> _raitingRepository;
        private readonly Repository<Comment> _commentRepository;
        private readonly Repository<News> _newsRepository;
        private readonly Repository<ProjectSubscriber> _projectSubscriberRepository;
        private readonly Repository<UserInfo> _userInfoRepository;
        private readonly IMapper<NewsViewModel, News> _newsMapper;
        private readonly IMapper<CommentViewModel, Comment> _commentMapper;
        private readonly IMapper<UserSmallViewModel, UserInfo> _userInfoMapper;
        private readonly IUserManager _userManager;
        private readonly ITagService _tagService;
        private readonly IPaymentManager _paymentManager;
        private readonly IFinancialPurposeManager _financialPurposeManager;

        public ProjectViewModelToProjectMapper(
            Repository<Rating> raitingRepository, Repository<Payment> paymentRepository,
            Repository<News> newsRepository, Repository<Comment> commentRepository,
            IMapper<CommentViewModel, Comment> commentMapper, IMapper<NewsViewModel, News> newsMapper,
            IUserManager userManager, Repository<ProjectSubscriber> projectSubscriberRepository,
            ITagService tagService, IPaymentManager paymentManager,
            IFinancialPurposeManager financialPurposeManager, Repository<UserInfo> userInfoRepository,
            IMapper<UserSmallViewModel, UserInfo> userInfoMapper)
        {
            _raitingRepository = raitingRepository;
            _paymentRepository = paymentRepository;
            _newsRepository = newsRepository;
            _commentRepository = commentRepository;
            _commentMapper = commentMapper;
            _newsMapper = newsMapper;
            _userManager = userManager;
            _projectSubscriberRepository = projectSubscriberRepository;
            _tagService = tagService;
            _paymentManager = paymentManager;
            _financialPurposeManager = financialPurposeManager;
            _userInfoRepository = userInfoRepository;
            _userInfoMapper = userInfoMapper;
        }

        public Project ConvertTo(ProjectViewModel item)
        {
            throw new NotImplementedException();
        }

        public ProjectViewModel ConvertFrom(Project item)
        {
            var project = new ProjectViewModel();
            ConvertFromBaseInformation(project, item, _userManager.CurrentUserName);
            ConvertFromCurrentUser(project, item, _userManager.CurrentUserName);
            ConvertFromPayment(project, item);
            ConvertFromCompleteObjects(project, item);
            return project;
        }

        private void ConvertFromCurrentUser(ProjectViewModel viewModel, Project model, string userName)
        {
            viewModel.IsSubscriber = _projectSubscriberRepository.FirstOrDefault(
                    subscriber => subscriber.UserName.Equals(userName) && subscriber.ProjectId.Equals(model.Id)) != null;
            ConvertFromRating(viewModel, model, userName);
        }

        private void ConvertFromBaseInformation(ProjectViewModel viewModel, Project model, string userName)
        {
            viewModel.Id = model.Id;
            viewModel.Name = model.Name;
            viewModel.Description = model.Description;
            viewModel.ImageUrl = model.ImageUrl;
            viewModel.Owner = new UserSmallViewModel { UserName = model.OwnerUserName};
            viewModel.Status = model.Status;
            viewModel.FundRaisingEnd = model.FundRaisingEnd;
        }

        private void ConvertFromPayment(ProjectViewModel viewModel, Project model)
        {
            var projectPayments = _paymentRepository.GetWhere(payment => payment.ProjectId == model.Id);
            viewModel.PaidAmount = _paymentManager.GetProjectPaidAmount(model.Id, projectPayments);
            viewModel.CountOfPayments = projectPayments.Count;
            viewModel.FinancialPurposes = _financialPurposeManager.GetProjectFinancialPurposeViewModels(model.Id, viewModel.PaidAmount);
        }

        private void ConvertFromCompleteObjects(ProjectViewModel viewModel, Project model)
        {
            viewModel.Tags = _tagService.GetProjectTags(model.Id);
            ConvertFromNews(viewModel, model.Id);
            ConvertFromComments(viewModel, model.Id);
        }

        private void ConvertFromRating(ProjectViewModel viewModel, Project model, string userName)
        {
            viewModel.Rating = _raitingRepository.FirstOrDefault(
                                    rating => rating.ProjectId == model.Id && rating.UserName == userName)
                                   ?.RatingResult ?? model.Ratings.Average(rating => rating.RatingResult);
        }

        private void ConvertFromNews(ProjectViewModel viewModel, string projectId)
        {
            viewModel.News = _newsRepository.GetWhere(news => news.ProjectId == projectId && news.Type == NewsType.News)
                .Select(news => _newsMapper.ConvertFrom(news));
        }

        private void ConvertFromComments(ProjectViewModel viewModel, string projectId)
        {
            var comments = _commentRepository.GetWhere(comment => comment.ProjectId == projectId);
            var commentators = comments.Select(comment => comment.UserName);
            var commentatorsInfo = _userInfoRepository.GetWhere(info => commentators.Contains(info.UserName));
            viewModel.Comments = comments.Select(comment => GetComment(commentatorsInfo, comment));
        }

        private CommentViewModel GetComment(IEnumerable<UserInfo> commentatorsInfo, Comment commentModel)
        {
            var commentViewModel = _commentMapper.ConvertFrom(commentModel);
            var userInfo = commentatorsInfo.FirstOrDefault(info => info.UserName == commentModel.UserName);
            commentViewModel.User = userInfo != null ? _userInfoMapper.ConvertFrom(userInfo) : null;
            return commentViewModel;
        }
    }
}