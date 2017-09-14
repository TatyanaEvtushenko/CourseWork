using System.Collections.Generic;
using Nest;

namespace CourseWork.BusinessLogicLayer.ElasticSearch.Documents
{
    [ElasticsearchType(Name = "projectSearchNote")]
    public class ProjectSearchNote
    {
        [Keyword(Name = "id", Index = true)]
        public string Id { get; set; }

        [Text(Name = "name", Index = true)]
        public string Name { get; set; }

        [Text(Name = "description", Index = true)]
        public string Description { get; set; }

        [Keyword(Name = "tag", Index = true)]
        public List<string> Tag { get; set; }

        [Text(Name = "newsSubject", Index = true)]
        public List<string> NewsSubject { get; set; }

        [Text(Name = "newsText", Index = true)]
        public List<string> NewsText { get; set; }

        [Text(Name = "comment", Index = true)]
        public List<string> Comment { get; set; }

        [Text(Name = "financialPurpose", Index = true)]
        public List<string> FinancialPurposeName { get; set; }

        [Text(Name = "financialPurposeDescription", Index = true)]
        public List<string> FinancialPurposeDescription { get; set; }
    }
}
