namespace CourseWork.DataLayer.Models
{
    public class ProjectSubscriber
    {
        public string ProjectId { get; set; }
        
        public string UserName { get; set; }

        public Project Project { get; set; }

        public UserInfo UserInfo { get; set; }
    }
}
