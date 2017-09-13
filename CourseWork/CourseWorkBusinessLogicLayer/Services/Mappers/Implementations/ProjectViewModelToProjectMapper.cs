using System;
using System.Linq;
using CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.NewsViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Microsoft.AspNetCore.Http;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class ProjectViewModelToProjectMapper : IMapper<ProjectViewModel, Project>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly Repository<Payment> _paymentRepository;
        private readonly Repository<Raiting> _raitingRepository;
        private readonly Repository<FinancialPurpose> _financialPurposeRepository;
        private readonly Repository<Tag> _tagRepository;
        private readonly Repository<Comment> _commentRepository;
        private readonly Repository<News> _newsRepository;
        private readonly IMapper<FinancialPurposeViewModel, FinancialPurpose> _financialPurposeMapper;
        private readonly IMapper<NewsViewModel, News> _newsMapper;
        private readonly IMapper<CommentViewModel, Comment> _commentMapper;

        public ProjectViewModelToProjectMapper(IHttpContextAccessor contextAccessor,
            Repository<Raiting> raitingRepository,
            IMapper<FinancialPurposeViewModel, FinancialPurpose> financialPurposeMapper,
            Repository<FinancialPurpose> financialPurposeRepository, Repository<Payment> paymentRepository,
            Repository<Tag> tagRepository, Repository<News> newsRepository, Repository<Comment> commentRepository,
            IMapper<CommentViewModel, Comment> commentMapper, IMapper<NewsViewModel, News> newsMapper)
        {
            _contextAccessor = contextAccessor;
            _raitingRepository = raitingRepository;
            _financialPurposeMapper = financialPurposeMapper;
            _financialPurposeRepository = financialPurposeRepository;
            _paymentRepository = paymentRepository;
            _tagRepository = tagRepository;
            _newsRepository = newsRepository;
            _commentRepository = commentRepository;
            _commentMapper = commentMapper;
            _newsMapper = newsMapper;
        }

        public Project ConvertTo(ProjectViewModel item)
        {
            throw new NotImplementedException();
        }

        public ProjectViewModel ConvertFrom(Project item)
        {
            var userName = _contextAccessor.HttpContext.User.Identity.Name;
            var project = new ProjectViewModel();
            ConvertFromBaseInformation(project, item, userName);
            ConvertFromPayment(project, item);
            ConvertFromCompleteObjects(project, item.Id);
            ConvertFromRating(project, item, userName);
            return project;
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
            ConvertFromFinancialPurposes(viewModel, projectId);
            ConvertFromTags(viewModel, projectId);
            ConvertFromNews(viewModel, projectId);
            ConvertFromComments(viewModel, projectId);
        }

        private void ConvertFromRating(ProjectViewModel viewModel, Project model, string userName)
        {
            viewModel.Rating = _raitingRepository.FirstOrDefault(
                                    rating => rating.ProjectId == model.Id && rating.UserName == userName)
                                   ?.RaitingResult ?? model.Raiting;
        }

        private void ConvertFromFinancialPurposes(ProjectViewModel viewModel, string projectId)
        {
            viewModel.FinancialPurposes = _financialPurposeRepository
                .GetWhere(purpose => purpose.ProjectId == projectId)
                .Select(purpose => _financialPurposeMapper.ConvertFrom(purpose));
        }

        private void ConvertFromTags(ProjectViewModel viewModel, string projectId)
        {
            viewModel.Tags = _tagRepository.GetWhere(tag => tag.ProjectId == projectId).Select(tag => tag.Name);
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