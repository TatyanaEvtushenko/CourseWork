using System;
using System.ComponentModel.DataAnnotations;
using CourseWork.DataLayer.Enums;

namespace CourseWork.DataLayer.Models
{
    public class News
    {
        [Key]
        public string Id { get; set; }

        public DateTime Time { get; set; }

        public string ProjectId { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        public NewsType Type { get; set; }

        public Project Project { get; set; }
    }
}
