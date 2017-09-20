using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CourseWork.BusinessLogicLayer.ElasticSearch;
using CourseWork.BusinessLogicLayer.ElasticSearch.Documents;
using CourseWork.BusinessLogicLayer.Services.Mappers;
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
        private readonly Repository<Project> _projectRepository;
        private readonly IMapper<ProjectItemViewModel, Project> _mapper;
        private readonly IMapper<ProjectSearchNote, Project> _projectSearchMapper;

        private delegate void UpdateNewsDelegate(News news, List<string> updatedNewsSubjects,
            List<string> updatedNewsTexts);
        private delegate void UpdateCommentDelegate(Comment comment, List<string> updatedCommentTexts);

        public SearchManager(SearchClient searchClient, IMapper<ProjectItemViewModel, Project> mapper,
            Repository<Project> projectRepository, IMapper<ProjectSearchNote, Project> projectSearchMapper)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _projectSearchMapper = projectSearchMapper;
            searchClient.CreateNewElasticClient();
            _client = searchClient.Client;
        }

        public IEnumerable<ProjectItemViewModel> Search(string query)
        {
            var response = _client.Search<ProjectSearchNote>(s => s.Query(q => q.MultiMatch(m => m.Fields(f => f
                    .Field(p => p.Name)
                    .Field(p => p.Comment)
                    .Field(p => p.Description)
                    .Field(p => p.FinancialPurposeDescription)
                    .Field(p => p.FinancialPurposeName)
                    .Field(p => p.NewsSubject).Field(p => p.NewsText)
                    .Field(p => p.Tag))
                .Query(query).Operator(Operator.Or))));
            return GetProjectsFromResponse(response);
        }

        public bool AddProjectToIndex(Project project)
        {
            var searchDocument = _projectSearchMapper.ConvertFrom(project);
            var response = _client.Index(searchDocument, p => p.Id(searchDocument.Id).Refresh(Refresh.True));
            return response.Result == Result.Created;
        }

        public bool AddNewsToIndex(News news)
        {
            return UpdateNews(news, AddNews);
        }

        public bool RemoveNewsFromIndex(News news)
        {
            return UpdateNews(news, RemoveNews);
        }

        public bool RemoveProjectsFromIndex(Project[] projects)
        {
            var projectDocuments = projects.Select(p => _projectSearchMapper.ConvertFrom(p)).ToArray();
            if (!projectDocuments.Any())
            {
                return true;
            }
            var response = _client.DeleteMany(projectDocuments);
            return !response.Errors;
        }

        public bool AddCommentToIndex(Comment comment)
        {
            return UpdateComment(comment, AddComment);
        }

        public bool RemoveCommentsFromIndex(Comment[] comments)
        {
            return comments.All(comment => UpdateComment(comment, RemoveComment));
        }

        public void SetFinancialPurposes(string projectId, FinancialPurpose[] purposes)
        {
            RefreshUpdateResponse(projectId, new
            {
                FinancialPurposeName = purposes.Select(p => p.Name).ToList(),
                FinancialPurposeDescription = purposes.Select(p => p.Description).ToList()
            });
        }

        public void SetTags(string projectId, string[] tags)
        {
            RefreshUpdateResponse(projectId, new {Tag = tags.ToList()});
        }

        private IEnumerable<ProjectItemViewModel> GetProjectsFromResponse(ISearchResponse<ProjectSearchNote> response)
        {
            var projectIds = response.Hits.Select(n => n.Source.Id).ToImmutableHashSet();
            return _projectRepository.GetWhere(project => projectIds.Contains(project.Id),
                    project => project.Subscribers, project => project.Payments, project=> project.Ratings)
                .Select(project => _mapper.ConvertFrom(project));
        }

        private IUpdateResponse<ProjectSearchNote> RefreshUpdateResponse(string projectId, object updateObject)
        {
            return _client.Update<ProjectSearchNote, Object>(projectId, d => d.Type("projectSearchNote")
                .Doc(updateObject).Refresh(Refresh.True));
        }
        
        private bool UpdateComment(Comment comment, UpdateCommentDelegate updateCommentAction)
        {
            var updatedCommentTexts = GetSearchDoc(comment.ProjectId).Comment;
            updateCommentAction(comment, updatedCommentTexts);
            var updateResponse = RefreshUpdateResponse(comment.ProjectId, new { Comment = updatedCommentTexts });
            return updateResponse.Result == Result.Updated;
        }

        private bool UpdateNews(News news, UpdateNewsDelegate updateNewsAction)
        {
            List<string> updatedNewsSubjects, updatedNewsTexts;
            GetNewsOptions(news.ProjectId, out updatedNewsSubjects, out updatedNewsTexts);
            updateNewsAction(news, updatedNewsSubjects, updatedNewsTexts);
            var updateResponse = RefreshUpdateResponse(news.ProjectId, new { NewsSubject = updatedNewsSubjects, NewsText = updatedNewsTexts});
            return updateResponse.Result == Result.Updated;
        }

        private void AddComment(Comment comment, List<string> updatedCommentTexts)
        {
            updatedCommentTexts.Add(comment.Text);
        }

        private void RemoveComment(Comment comment, List<string> updatedCommentTexts)
        {
            updatedCommentTexts.RemoveAt(updatedCommentTexts.IndexOf(comment.Text));
        }

        private void AddNews(News news, List<string> updatedNewsSubjects, List<string> updatedNewsTexts)
        {
            updatedNewsSubjects.Add(news.Subject);
            updatedNewsTexts.Add(news.Text);
        }

        private void RemoveNews(News news, List<string> updatedNewsSubjects, List<string> updatedNewsTexts)
        {
            updatedNewsSubjects.RemoveAt(updatedNewsSubjects.IndexOf(news.Subject));
            updatedNewsTexts.RemoveAt(updatedNewsTexts.IndexOf(news.Text));
        }

        private void GetNewsOptions(object projectId, out List<string> updatedNewsSubjects, out List<string> updatedNewsTexts)
        {
            var doc = GetSearchDoc(projectId);
            updatedNewsSubjects = doc.NewsSubject;
            updatedNewsTexts = doc.NewsText;
        }

        private ProjectSearchNote GetSearchDoc(object projectId)
        {
            var searchResponse = _client.Search<ProjectSearchNote>(s =>
                s.Type("projectSearchNote").Query(q => q.Term(t => t.Field("id").Value(projectId))));
            return searchResponse.Hits.Select(n => n.Source).Single();
        }
    }
}
