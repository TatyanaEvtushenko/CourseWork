using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CourseWork.BusinessLogicLayer.ElasticSearch;
using CourseWork.BusinessLogicLayer.ElasticSearch.Documents;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.PaymentManagers;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.NewsViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Elasticsearch.Net;
using Nest;

namespace CourseWork.BusinessLogicLayer.Services.SearchManagers.Implementations
{
    public class SearchManager : ISearchManager
    {
        private readonly ElasticClient _client;
        private readonly IMapper<ProjectItemViewModel, Project> _mapper;
        private readonly Repository<Project> _projectRepository;
        private readonly IUserManager _userManager;
        private readonly IMapper<ProjectSearchNote, Project> _projectSearchMapper;
        private readonly IPaymentManager _paymentManager;

        public SearchManager(SearchClient searchClient, IMapper<ProjectItemViewModel, Project> mapper, Repository<Project> projectRepository, IMapper<ProjectSearchNote, Project> projectSearchMapper, IUserManager userManager, IPaymentManager paymentManager)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _projectSearchMapper = projectSearchMapper;
            _userManager = userManager;
            _paymentManager = paymentManager;
            searchClient.CreateNewElasticClient();
            _client = searchClient.Client;
        }

        public IEnumerable<ProjectItemViewModel> Search(string query)
        {
            var response = _client.Search<ProjectSearchNote>(s => s.Query(q => q.MultiMatch(m => m
                .Fields(f => f.Field(p => p.Name).Field(p => p.Comment).Field(p => p.Description).Field(p => p.FinancialPurposeDescription)
                    .Field(p => p.FinancialPurposeName).Field(p => p.NewsSubject).Field(p => p.NewsText).Field(p => p.Tag))
                    .Query(query).Operator(Operator.Or))));
            var projectIds = response.Hits.Select(n => n.Source.Id).ToImmutableHashSet();
            return _projectRepository
                .GetWhereEager<IEnumerable<Object>>(item => projectIds.Contains(item.Id), item => item.Subscribers, item => item.Payments)
                .Select(item =>
            {
                var viewModel = _mapper.ConvertFrom(item);
                viewModel.IsSubscriber = item.Subscribers?.FirstOrDefault(s => s.UserName == _userManager.CurrentUserName) != null;
                viewModel.PaidAmount = _paymentManager.GetProjectPaidAmount(item.Id, item.Payments);
                return viewModel;
            });
        }

        public bool AddProjectToIndex(Project project)
        {
            var searchDocument = _projectSearchMapper.ConvertFrom(project);
            var response = _client.Index(searchDocument, p => p.Id(searchDocument.Id).Refresh(Refresh.True));
            return response.Result == Result.Created;
        }

        public bool AddNewsToIndex(News news)
        {
            var searchResponse = _client.Search<ProjectSearchNote>(s =>
                s.Type("projectSearchNote").Query(q => q.Term(t => t.Field("id").Value(news.ProjectId))));
            var doc = searchResponse.Hits.Select(n => n.Source).Single();
            var updatedNewsSubjects = doc.NewsSubject;
            var updatedNewsTexts = doc.NewsText;
            updatedNewsSubjects.Add(news.Subject);
            updatedNewsTexts.Add(news.Text);
            var updateResponse = _client.Update<ProjectSearchNote, Object>(news.ProjectId, d => d.Type("projectSearchNote")
                .Doc(new { NewsSubject = updatedNewsSubjects, NewsText = updatedNewsTexts }).Refresh(Refresh.True));
            return updateResponse.Result == Result.Updated;
        }

        public bool RemoveNewsFromIndex(News news)
        {
            var searchResponse = _client.Search<ProjectSearchNote>(s =>
                s.Type("projectSearchNote").Query(q => q.Term(t => t.Field("id").Value(news.ProjectId))));
            var doc = searchResponse.Hits.Select(n => n.Source).Single();
            var updatedNewsSubjects = doc.NewsSubject;
            var updatedNewsTexts = doc.NewsText;
            updatedNewsSubjects.RemoveAt(updatedNewsSubjects.IndexOf(news.Subject));
            updatedNewsTexts.RemoveAt(updatedNewsTexts.IndexOf(news.Text));
            var updateResponse = _client.Update<ProjectSearchNote, Object>(news.ProjectId, d => d.Type("projectSearchNote")
                .Doc(new { NewsSubject = updatedNewsSubjects, NewsText = updatedNewsTexts }).Refresh(Refresh.True));
            return updateResponse.Result == Result.Updated;
        }

        public bool RemoveProjectsFromIndex(Project[] projects)
        {
            var projectDocuments = projects.Select(p => _projectSearchMapper.ConvertFrom(p));
            var response = _client.DeleteMany(projectDocuments);
            return !response.Errors;
        }

        public bool AddCommentToIndex(Comment comment)
        {
            var searchResponse = _client.Search<ProjectSearchNote>(s =>
                s.Type("projectSearchNote").Query(q => q.Term(t => t.Field("id").Value(comment.ProjectId))));
            var doc = searchResponse.Hits.Select(n => n.Source).Single();
            var updatedCommentTexts = doc.Comment;
            updatedCommentTexts.Add(comment.Text);
            var updateResponse = _client.Update<ProjectSearchNote, Object>(comment.ProjectId, d => d.Type("projectSearchNote")
                .Doc(new { Comment = updatedCommentTexts }).Refresh(Refresh.True));
            return updateResponse.Result == Result.Updated;
        }

        public bool RemoveCommentsFromIndex(Comment[] comments)
        {
            foreach (var comment in comments)
            {
                var searchResponse = _client.Search<ProjectSearchNote>(s =>
                    s.Type("projectSearchNote").Query(q => q.Term(t => t.Field("id").Value(comment.ProjectId))));
                var doc = searchResponse.Hits.Select(n => n.Source).Single();
                var updatedCommentTexts = doc.Comment;
                updatedCommentTexts.RemoveAt(updatedCommentTexts.IndexOf(comment.Text));
                var updateResponse = _client.Update<ProjectSearchNote, Object>(comment.ProjectId, d => d.Type("projectSearchNote")
                    .Doc(new { Comment = updatedCommentTexts }).Refresh(Refresh.True));
                if (updateResponse.Result != Result.Updated) return false;
            }
            return true;
        }

        public void SetFinancialPurposes(string projectId, FinancialPurpose[] purposes)
        {
            _client.Update<ProjectSearchNote, Object>(projectId, d => d.Type("projectSearchNote")
                .Doc(new { FinancialPurposeName = purposes.Select(p => p.Name).ToList(), FinancialPurposeDescription = purposes.Select(p => p.Description).ToList() })
                .Refresh(Refresh.True));
        }

        public void SetTags(string projectId, string[] tags)
        {
            _client.Update<ProjectSearchNote, Object>(projectId, d => d.Type("projectSearchNote")
                .Doc(new { Tag = tags.ToList() }).Refresh(Refresh.True));
        }
    }
}
