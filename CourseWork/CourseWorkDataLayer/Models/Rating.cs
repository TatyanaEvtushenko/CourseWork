using System.ComponentModel.DataAnnotations;

namespace CourseWork.DataLayer.Models
{
    public class Rating
    {
		[Key]
        public string ProjectId { get; set; }

        [Key]
        public string UserName { get; set; }

        public int RatingResult { get; set; }
    }
}
