using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.ElasticSearch;
using CourseWork.BusinessLogicLayer.ElasticSearch.Documents;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.MessageSenders;
using CourseWork.BusinessLogicLayer.Services.PaymentManagers;
using CourseWork.BusinessLogicLayer.Services.ProjectManagers;
using CourseWork.BusinessLogicLayer.Services.ProjectSubscriberManagers;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.NewsViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Elasticsearch.Net;
using Nest;

namespace CourseWork.BusinessLogicLayer.Services.NewsManagers.Implementations
{
    public class NewsManager : INewsManager
    {
        private readonly Repository<News> _newsRepository;
        private readonly IMapper<NewsFormViewModel, News> _newsMapper;
        private readonly IEmailSender _emailSender;
        private readonly IProjectManager _projectManager;
        private readonly IPaymentManager _paymentManager;
        private readonly IProjectSubscriberManager _projectSubscriberManager;
        private readonly IUserManager _userManager;
        private readonly ElasticClient _client;

        public NewsManager(Repository<News> newsRepository, IMapper<NewsFormViewModel, News> newsMapper,
            IEmailSender emailSender, IProjectManager projectManager, IPaymentManager paymentManager,
            IProjectSubscriberManager projectSubscriberManager, IUserManager userManager, SearchClient searchClient)
        {
            _newsRepository = newsRepository;
            _newsMapper = newsMapper;
            _emailSender = emailSender;
            _projectManager = projectManager;
            _paymentManager = paymentManager;
            _projectSubscriberManager = projectSubscriberManager;
            _userManager = userManager;
            searchClient.CreateNewElasticClient();
            _client = searchClient.Client;
        }

        public bool AddNews(NewsFormViewModel newsForm)
        {
            return AddNewsToRepository(newsForm, NewsType.News);
        }

        public async Task<bool> AddMailingToSubscribers(NewsFormViewModel newsForm)
        {
            var recipientUserNames = _projectSubscriberManager.GetSubscribers(newsForm.ProjectId)
                .Select(subscriber => subscriber.UserName);
            await SendMailing(newsForm, recipientUserNames);
            return AddNewsToRepository(newsForm, NewsType.MailingToSubscribers);
        }

        public async Task<bool> AddMailingToPayers(NewsFormViewModel newsForm)
        {
            var recipientUserNames = _paymentManager.GetProjectPayments(newsForm.ProjectId)
                .Select(payment => payment.UserName);
            await SendMailing(newsForm, recipientUserNames);
            return AddNewsToRepository(newsForm, NewsType.MailingToPayers);
        }

        private bool AddNewsToRepository(NewsFormViewModel newsForm, NewsType type)
        {
            var news = GetPreparedNews(newsForm, type);
            AddNewsToIndex(news);
            return _newsRepository.AddRange(news);
        }

        private void AddNewsToIndex(News news)
        {
            var response = _client.Search<ProjectSearchNote>(s =>
                s.Type("projectSearchNote").Query(q => q.Term(t => t.Field("id").Value(news.ProjectId))));
            var doc = response.Hits.Select(n => n.Source).Single();
            var updatedNewsSubjects = doc.NewsSubject;
            var updatedNewsTexts = doc.NewsText;
            updatedNewsSubjects.Add(news.Subject);
            updatedNewsTexts.Add(news.Text);
            _client.Update<ProjectSearchNote, Object>(news.ProjectId, d => d.Type("projectSearchNote")
                .Doc(new {NewsSubject = updatedNewsSubjects, NewsText = updatedNewsTexts}).Refresh(Refresh.True));
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
            var projectName = _projectManager.GetProjectName(newsForm.ProjectId);
            return $"From \"{projectName}\" project: {newsForm.Subject}";
        }

        private News GetPreparedNews(NewsFormViewModel newsForm, NewsType type)
        {
            var news = _newsMapper.ConvertTo(newsForm);
            news.Id = _newsRepository.GetNewId();
            news.Time = DateTime.Now;
            news.Type = type;
            return news;
        }
    }
}