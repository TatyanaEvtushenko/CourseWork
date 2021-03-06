﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Options;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.MessageManagers;
using CourseWork.BusinessLogicLayer.Services.MessageSenders;
using CourseWork.BusinessLogicLayer.Services.SearchManagers;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.MessageViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.NewsViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Microsoft.Extensions.Options;

namespace CourseWork.BusinessLogicLayer.Services.NewsManagers.Implementations
{
    public class NewsManager : INewsManager
    {
        private readonly IRepository<News> _newsRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IMapper<NewsFormViewModel, News> _newsMapper;
        private readonly IMapper<NewsViewModel, News> _newsViewMapper;
        private readonly IEmailSender _emailSender;
        private readonly IUserManager _userManager;
        private readonly ISearchManager _searchManager;
        private readonly HomePageOptions _options;
        private readonly IMessageManager _messageManager;

        public NewsManager(IRepository<News> newsRepository, IMapper<NewsFormViewModel, News> newsMapper,
            IEmailSender emailSender, IRepository<Project> projectRepository,
            IUserManager userManager, ISearchManager searchManager, IMapper<NewsViewModel, News> newsViewMapper, IOptions<HomePageOptions> options, IMessageManager messageManager)
        {
            _newsRepository = newsRepository;
            _newsMapper = newsMapper;
            _emailSender = emailSender;
            _userManager = userManager;
            _projectRepository = projectRepository;
            _searchManager = searchManager;
            _newsViewMapper = newsViewMapper;
            _messageManager = messageManager;
            _options = options.Value;
        }

        public IEnumerable<NewsViewModel> GetLastNews()
        {
            var newsModels = _newsRepository.GetOrdered(news => news.Time, _options.LatestNewsCount, true,
                news => news.Project);
            return newsModels.Select(news => _newsViewMapper.ConvertFrom(news));
        }

        public bool AddNews(NewsFormViewModel newsForm, string message)
        {
            var result = AddNewsToRepository(newsForm, NewsType.News);
            if (result)
            {
                _messageManager.NotifySubscribers(new SubscriberNotificationViewModel
                {
                    Id = newsForm.ProjectId,
                    Text = message,
                    Subject = newsForm.Subject
                });
            }
            return result;
        }

        public async Task<bool> AddMailingToSubscribers(NewsFormViewModel newsForm)
        {
            var project = _projectRepository.FirstOrDefault(p => p.Id == newsForm.ProjectId, p => p.Subscribers);
            var recipientUserNames = project.Subscribers.Select(subscriber => subscriber.UserName);
            await SendMailing(newsForm, recipientUserNames, project.Name);
            return AddNewsToRepository(newsForm, NewsType.MailingToSubscribers);
        }

        public async Task<bool> AddMailingToPayers(NewsFormViewModel newsForm)
        {
            var project = _projectRepository.FirstOrDefault(p => p.Id == newsForm.ProjectId, p => p.Payments);
            var recipientUserNames = project.Payments.Select(payment => payment.UserName);
            await SendMailing(newsForm, recipientUserNames, project.Name);
            return AddNewsToRepository(newsForm, NewsType.MailingToPayers);
        }

        public bool RemoveNews(string newsId)
        {
            var news = _newsRepository.FirstOrDefault(n => n.Id == newsId);
            return _newsRepository.RemoveRange(newsId) && _searchManager.RemoveNewsFromIndex(news);
        }

        private bool AddNewsToRepository(NewsFormViewModel newsForm, NewsType type)
        {
            var news = _newsMapper.ConvertTo(newsForm);
            news.Type = type;
            return _newsRepository.AddRange(news) && _searchManager.AddNewsToIndex(news);
        }

        private async Task SendMailing(NewsFormViewModel newsForm, IEnumerable<string> recipientUserNames, string projectName)
        {
            var recipientEmails = _userManager.GetEmails(recipientUserNames);
            var subject = GetSubjectForLetter(projectName, newsForm.Subject);
            var message = CommonMark.CommonMarkConverter.Convert(newsForm.Text);
            foreach (var recipientEmail in recipientEmails)
            {
                await _emailSender.SendEmailAsync(recipientEmail, subject, message);
            }
        }

        private string GetSubjectForLetter(string projectName, string subject) =>
            $"From \"{projectName}\" project: {subject}";
    }
}