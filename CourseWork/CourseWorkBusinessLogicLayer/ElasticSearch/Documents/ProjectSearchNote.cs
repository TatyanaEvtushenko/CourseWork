using Nest;

namespace CourseWork.BusinessLogicLayer.ElasticSearch.Documents
{
    [ElasticsearchType(Name = "projectSearchNote")]
    public class ProjectSearchNote
    {
        [Keyword(Name = "id", Index = false)]
        public string Id { get; set; }

        [Text(Name = "name", Index = true)]
        public string Name { get; set; }

        [Text(Name = "description", Index = true)]
        public string Description { get; set; }

        [Keyword(Name = "tag", Index = true)]
        public string Tag { get; set; }

        [Text(Name = "newsSubject", Index = true)]
        public string NewsSubject { get; set; }

        [Text(Name = "newsText", Index = true)]
        public string NewsText { get; set; }

        [Text(Name = "comment", Index = true)]
        public string Comment { get; set; }
    }
}
