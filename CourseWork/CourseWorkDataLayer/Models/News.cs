using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CourseWork.DataLayer.Enums;

namespace CourseWork.DataLayer.Models
{
    public class News
    {
        public string Id { get; set; }

        public DateTime Time { get; set; }

        public string ProjectId { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        public NewsType Type { get; set; }

        [ForeignKey("ProjectId")]
        [Required]
        public Project Project { get; set; }
    }
}
