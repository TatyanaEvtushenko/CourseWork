using System;
using CourseWork.BusinessLogicLayer.ElasticSearch.Documents;
using Nest;
using Microsoft.Extensions.Options;
using CourseWork.BusinessLogicLayer.Options;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using CourseWork.DataLayer.Repositories.Implementations;
using Elasticsearch.Net;

namespace CourseWork.BusinessLogicLayer.ElasticSearch
{ 
    public class SearchClient
    {
        public ElasticClient Client { get; private set; }
        private readonly ElasticSearchOptions _options;
        private readonly IMapper<ProjectSearchNote, Project> _projectSearchMapper;
        private readonly Repository<Project> _projectRepository;

        public SearchClient(IOptions<ElasticSearchOptions> options, Repository<Project> projectRepository, IMapper<ProjectSearchNote, Project> projectSearchMapper)
        {
            _projectRepository = projectRepository;
            _projectSearchMapper = projectSearchMapper;
            _options = options.Value;
        }

        public void CreateNewElasticClient()
        {
            var node = new Uri(_options.Uri);
            var settings = new ConnectionSettings(node);
            settings.DefaultIndex(_options.DefaultIndex);
            Client = new ElasticClient(settings);
            if (_options.Repopulate)
            {
                Client.DeleteIndex(_options.DefaultIndex);
                var indexDescriptor =
                        new CreateIndexDescriptor(_options.DefaultIndex).Mappings(ms =>
                            ms.Map<ProjectSearchNote>(m => m.AutoMap()));
                Client.CreateIndex(indexDescriptor);
                Repopulate();
            }
        }

        private void Repopulate()
        {
            foreach (var item in _projectRepository.GetAll())
            {
                var searchDocument = _projectSearchMapper.ConvertFrom(item);
                Client.Index(searchDocument, p => p.Id(searchDocument.Id).Refresh(Refresh.True));
            }
        }
    }
}
