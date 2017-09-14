using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CourseWork.BusinessLogicLayer.ElasticSearch;
using CourseWork.BusinessLogicLayer.ElasticSearch.Documents;
using CourseWork.BusinessLogicLayer.Services.Mappers;
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
        private readonly IMapper<ProjectSearchNote, Project> _projectSearchMapper;

        public SearchManager(SearchClient searchClient, IMapper<ProjectItemViewModel, Project> mapper, Repository<Project> projectRepository, IMapper<ProjectSearchNote, Project> projectSearchMapper)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _projectSearchMapper = projectSearchMapper;
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
            return _projectRepository.GetWhere(n => projectIds.Contains(n.Id)).Select(n => _mapper.ConvertFrom(n));
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
    }
}
