using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CourseWork.BusinessLogicLayer.ElasticSearch;
using CourseWork.BusinessLogicLayer.ElasticSearch.Documents;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Nest;

namespace CourseWork.BusinessLogicLayer.Services.SearchManagers.Implementations
{
    public class SearchManager : ISearchManager
    {
        private readonly ElasticClient _client;
        private readonly IMapper<ProjectItemViewModel, Project> _mapper;
        private readonly Repository<Project> _projectRepository;

        public SearchManager(SearchClient searchClient, IMapper<ProjectItemViewModel, Project> mapper, Repository<Project> projectRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            searchClient.CreateNewElasticClient();
            _client = searchClient.Client;
        }

        public IEnumerable<ProjectSearchNote> Search(string query)
        {
            var response = _client.Search<ProjectSearchNote>(s => s.Query(q => q.MultiMatch(m => m
                .Fields(f => f.Field(p => p.Name).Field(p => p.Comment).Field(p => p.Description).Field(p => p.FinancialPurposeDescription)
                    .Field(p => p.FinancialPurposeName).Field(p => p.NewsSubject).Field(p => p.NewsText).Field(p => p.Tag))
                    .Query(query).Operator(Operator.Or))));
            var projectIds = response.Hits.Select(n => n.Source.Id).ToImmutableHashSet();
            return
                response.Hits.Select(n => n.Source); //_projectRepository.GetWhere(n => projectIds.Contains(n.Id)).Select(n => _mapper.ConvertFrom(n));
        }
    }
}
