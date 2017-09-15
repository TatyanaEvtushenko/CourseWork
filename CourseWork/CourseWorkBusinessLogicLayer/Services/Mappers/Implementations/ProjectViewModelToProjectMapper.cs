using System;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.FinancialPurposeManagers;
using CourseWork.BusinessLogicLayer.Services.TagServices;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;
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
        private readonly IMapper<NewsViewModel, News> _newsMapper;
        private readonly IMapper<CommentViewModel, Comment> _commentMapper;
        private readonly IUserManager _userManager;
        private readonly IFinancialPurposeManager _financialPurposeManager;
        private readonly ITagService _tagService;

        public ProjectViewModelToProjectMapper(
            Repository<Rating> raitingRepository, Repository<Payment> paymentRepository,
            Repository<News> newsRepository, Repository<Comment> commentRepository,
            IMapper<CommentViewModel, Comment> commentMapper, IMapper<NewsViewModel, News> newsMapper,
            IUserManager userManager, Repository<ProjectSubscriber> projectSubscriberRepository,
            IFinancialPurposeManager financialPurposeManager, ITagService tagService)
        {
            _raitingRepository = raitingRepository;
            _paymentRepository = paymentRepository;
            _newsRepository = newsRepository;
            _commentRepository = commentRepository;
            _commentMapper = commentMapper;
            _newsMapper = newsMapper;
            _userManager = userManager;
            _projectSubscriberRepository = projectSubscriberRepository;
            _financialPurposeManager = financialPurposeManager;
            _tagService = tagService;
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
            ConvertFromCompleteObjects(project, item.Id);
            return project;
        }

        private void ConvertFromCurrentUser(ProjectViewModel viewModel, Project model, string userName)
        {
            viewModel.IsSubscriber = _projectSubscriberRepository.FirstOrDefault(
                    subscriber => subscriber.UserName == userName && subscriber.ProjectId == model.Id) != null;
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
            viewModel.MaxPaymentAmount = model.MaxPayment;
            viewModel.MinPaymentAmount = model.MinPayment;
            viewModel.PaidAmount = model.PaidAmount;
            viewModel.CountOfPayments = _paymentRepository.Count(payment => payment.ProjectId == model.Id);
        }

        private void ConvertFromCompleteObjects(ProjectViewModel viewModel, string projectId)
        {
            viewModel.FinancialPurposes = _financialPurposeManager.GetProjectFinancialPurposees(projectId);
            viewModel.Tags = _tagService.GetProjectTags(projectId);
            ConvertFromNews(viewModel, projectId);
            ConvertFromComments(viewModel, projectId);
        }

        private void ConvertFromRating(ProjectViewModel viewModel, Project model, string userName)
        {
            viewModel.Rating = _raitingRepository.FirstOrDefault(
                                    rating => rating.ProjectId == model.Id && rating.UserName == userName)
                                   ?.RatingResult ?? model.Rating;
        }

        private void ConvertFromNews(ProjectViewModel viewModel, string projectId)
        {
            viewModel.News = _newsRepository.GetWhere(news => news.ProjectId == projectId && news.Type == NewsType.News)
                .Select(news => _newsMapper.ConvertFrom(news));
        }

        private void ConvertFromComments(ProjectViewModel viewModel, string projectId)
        {
            viewModel.Comments = _commentRepository.GetWhere(comment => comment.ProjectId == projectId)
                .Select(comment => _commentMapper.ConvertFrom(comment));
        }
    }
}