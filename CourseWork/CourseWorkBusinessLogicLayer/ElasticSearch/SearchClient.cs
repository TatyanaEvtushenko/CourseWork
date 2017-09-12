using System;
using Nest;
using Microsoft.Extensions.Options;
using CourseWork.BusinessLogicLayer.Options;

namespace CourseWork.BusinessLogicLayer.ElasticSearch
{ 
    public class SearchClient
    {
        public ElasticClient Client { get; private set; }
        private readonly ElasticSearchOptions _options;

        public SearchClient(IOptions<ElasticSearchOptions> options)
        {
            _options = options.Value;
        }

        public void CreateNewElasticClient()
        {
            var node = new Uri(_options.Uri);
            var settings = new ConnectionSettings(node);
            settings.DefaultIndex(_options.DefaultIndex);
            Client = new ElasticClient(settings);
            //Client.DeleteIndex(_options.DefaultIndex);
            if (!Client.IndexExists(_options.DefaultIndex).Exists)
            {
                var indexDescriptor = new CreateIndexDescriptor(_options.DefaultIndex).Mappings(ms => ms.Map<ProjectSearchNote>(m => m.AutoMap()));
                Client.CreateIndex(indexDescriptor);
            }
        }
    }
}
