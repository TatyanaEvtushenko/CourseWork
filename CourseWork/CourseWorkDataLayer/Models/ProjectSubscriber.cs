using System.ComponentModel.DataAnnotations;

namespace CourseWork.DataLayer.Models
{
    public class ProjectSubscriber
    {
        [Key]
        public string ProjectId { get; set; }

        [Key]
        public string UserName { get; set; }
    }
}
