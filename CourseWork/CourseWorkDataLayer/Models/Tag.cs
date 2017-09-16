using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.DataLayer.Models
{
    public class Tag
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        [Required]
        public Project Project { get; set; }
    }
}
