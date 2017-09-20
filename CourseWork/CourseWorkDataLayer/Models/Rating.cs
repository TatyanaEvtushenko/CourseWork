using System.ComponentModel.DataAnnotations;

namespace CourseWork.DataLayer.Models
{
    public class Rating
    {
        [Key]
        public string Id { get; set; }

        public string ProjectId { get; set; }
        
        public string UserName { get; set; }

        public int RatingResult { get; set; }

        public Project Project { get; set; }

        public UserInfo UserInfo { get; set; }
    }
}
