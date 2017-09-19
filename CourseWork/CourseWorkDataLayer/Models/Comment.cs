using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.DataLayer.Models
{
    public class Comment
    {
		[Key]
        public string Id { get; set; }

        public string UserName { get; set; }

        public string ProjectId { get; set; }

        public DateTime Time { get; set; }

        public string Text { get; set; }

        [ForeignKey("ProjectId")]
        [Required]
        public Project Project { get; set; }
    }
}
