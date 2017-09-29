namespace CourseWork.DataLayer.Models
{
    public class Tag
    {
        public string Name { get; set; }

        public string ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
