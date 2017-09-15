using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CourseWork.DataLayer.Models
{
    public class ProjectSubscriber
    {
        public string Id { get; set; }

        public string ProjectId { get; set; }

        public string UserName { get; set; }

        [ForeignKey("ProjectId")]
        [Required]
        public Project Project { get; set; }
    }
}
