using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CourseWork.DataLayer.Models
{
    public class ProjectSubscriber
    {
        [Key]
        public string ProjectId { get; set; }

        [Key]
        public string UserName { get; set; }

        [ForeignKey("ProjectId")]
        [Required]
        public Project Project { get; set; }
    }
}
