using System.ComponentModel.DataAnnotations;

namespace CourseWork.DataLayer.Models
{
    public class Tag
    {
        [Key]
        public string Name { get; set; }

        [Key]
        public string ProjectId { get; set; }
    }
}
