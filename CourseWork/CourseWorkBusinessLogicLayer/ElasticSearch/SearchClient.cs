using System;
using CourseWork.BusinessLogicLayer.ElasticSearch.Documents;
using Nest;
using Microsoft.Extensions.Options;
using CourseWork.BusinessLogicLayer.Options;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Elasticsearch.Net;

namespace CourseWork.BusinessLogicLayer.ElasticSearch
{ 
    public class SearchClient
    {
        public ElasticClient Client { get; private set; }
        private readonly ElasticSearchOptions _options;
        private readonly IMapper<ProjectSearchNote, Project> _projectSearchMapper;
        private readonly Repository<Project> _projectRepository;

        public SearchClient(IOptions<ElasticSearchOptions> options, Repository<Project> projectRepository,
            IMapper<ProjectSearchNote, Project> projectSearchMapper)
        {
            _projectRepository = projectRepository;
            _projectSearchMapper = projectSearchMapper;
            _options = options.Value;
        }

        public void CreateNewElasticClient()
        {
            Client = CreateClient();
            if (Client.IndexExists(_options.DefaultIndex).Exists)
            {
                return;
            }
            CreateIndex();
            Repopulate();
        }

        private ElasticClient CreateClient()
        {
            var node = new Uri(_options.Uri);
            var settings = new ConnectionSettings(node);
            settings.DefaultIndex(_options.DefaultIndex);
            return new ElasticClient(settings);
        }

        private void CreateIndex()
        {
            var indexDescriptor = new CreateIndexDescriptor(_options.DefaultIndex);
            var mappedDescriptor = indexDescriptor.Mappings(ms => ms.Map<ProjectSearchNote>(m => m.AutoMap()));
            Client.CreateIndex(mappedDescriptor);
        }

        private void Repopulate()
        {
            var projects = _projectRepository.GetAll(
                p => p.Comments, p => p.FinancialPurposes, p => p.News, p => p.Tags);
            foreach (var project in projects)
            {
                var searchDocument = _projectSearchMapper.ConvertFrom(project);
                Client.Index(searchDocument, p => p.Id(searchDocument.Id).Refresh(Refresh.True));
            }
        }
    }
}
