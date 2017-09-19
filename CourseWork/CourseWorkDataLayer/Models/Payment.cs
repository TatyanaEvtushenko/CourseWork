using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.DataLayer.Models
{
    public class Payment
    {
        public string Id { get; set; }

        public string ProjectId { get; set; }

        public string UserName { get; set; }

        public DateTime Time { get; set; }

        public decimal PaidAmount { get; set; }

        [ForeignKey("ProjectId")]
        [Required]
        public Project Project { get; set; }

        [ForeignKey("UserName")]
        [Required]
        public UserInfo UserInfo { get; set; }
    }
}
