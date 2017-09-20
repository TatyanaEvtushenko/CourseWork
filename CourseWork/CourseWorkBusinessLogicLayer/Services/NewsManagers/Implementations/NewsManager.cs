using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.MessageSenders;
using CourseWork.BusinessLogicLayer.Services.PaymentManagers;
using CourseWork.BusinessLogicLayer.Services.ProjectSubscriberManagers;
using CourseWork.BusinessLogicLayer.Services.SearchManagers;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.NewsViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.NewsManagers.Implementations
{
    public class NewsManager : INewsManager
    {
        private readonly Repository<News> _newsRepository;
        private readonly Repository<Project> _projectRepository;
        private readonly IMapper<NewsFormViewModel, News> _newsMapper;
        private readonly IMapper<NewsViewModel, News> _newsViewMapper;
        private readonly IEmailSender _emailSender;
        private readonly IPaymentManager _paymentManager;
        private readonly IProjectSubscriberManager _projectSubscriberManager;
        private readonly IUserManager _userManager;
        private readonly ISearchManager _searchManager;

        public NewsManager(Repository<News> newsRepository, IMapper<NewsFormViewModel, News> newsMapper,
            IEmailSender emailSender, Repository<Project> projectRepository, IPaymentManager paymentManager,
            IProjectSubscriberManager projectSubscriberManager, IUserManager userManager, ISearchManager searchManager, IMapper<NewsViewModel, News> newsViewMapper)
        {
            _newsRepository = newsRepository;
            _newsMapper = newsMapper;
            _emailSender = emailSender;
            _paymentManager = paymentManager;
            _projectSubscriberManager = projectSubscriberManager;
            _userManager = userManager;
            _projectRepository = projectRepository;
            _searchManager = searchManager;
            _newsViewMapper = newsViewMapper;
        }

        public IEnumerable<NewsViewModel> GetLastNews()
        {
            return _newsRepository.GetWhere(news => news.Time.AddDays(2) >= DateTime.UtcNow,
                news => news.Project)
                .Select(news => _newsViewMapper.ConvertFrom(news));
        }

        public bool AddNews(NewsFormViewModel newsForm)
        {
            return AddNewsToRepository(newsForm, NewsType.News);
        }

        public async Task<bool> AddMailingToSubscribers(NewsFormViewModel newsForm)
        {
            var recipientUserNames = _projectSubscriberManager.GetSubscribers(newsForm.ProjectId).Select(subscriber => subscriber.UserName);
            await SendMailing(newsForm, recipientUserNames);
            return AddNewsToRepository(newsForm, NewsType.MailingToSubscribers);
        }

        public async Task<bool> AddMailingToPayers(NewsFormViewModel newsForm)
        {
            var recipientUserNames = _paymentManager.GetProjectPayments(newsForm.ProjectId).Select(payment => payment.UserName);
            await SendMailing(newsForm, recipientUserNames);
            return AddNewsToRepository(newsForm, NewsType.MailingToPayers);
        }

        public bool RemoveNews(string newsId)
        {
            var news = _newsRepository.Get(newsId);
            return _newsRepository.RemoveRange(newsId) && _searchManager.RemoveNewsFromIndex(news);
        }

        private bool AddNewsToRepository(NewsFormViewModel newsForm, NewsType type)
        {
            var news = GetPreparedNews(newsForm, type);
            return _newsRepository.AddRange(news) && _searchManager.AddNewsToIndex(news);
        }

        private async Task SendMailing(NewsFormViewModel newsForm, IEnumerable<string> recipientUserNames)
        {
            var recipientEmails = _userManager.GetEmails(recipientUserNames);
            var subject = GetSubjectForLetter(newsForm);
            var message = CommonMark.CommonMarkConverter.Convert(newsForm.Text);
            foreach (var recipientEmail in recipientEmails)
            {
                await _emailSender.SendEmailAsync(recipientEmail, subject, message);
            }
        }

        private string GetSubjectForLetter(NewsFormViewModel newsForm)
        {
            var projectName = _projectRepository.Get(newsForm.ProjectId).Name;
            return $"From \"{projectName}\" project: {newsForm.Subject}";
        }

        private News GetPreparedNews(NewsFormViewModel newsForm, NewsType type)
        {
            var news = _newsMapper.ConvertTo(newsForm);
            news.Type = type;
            return news;
        }
    }
}