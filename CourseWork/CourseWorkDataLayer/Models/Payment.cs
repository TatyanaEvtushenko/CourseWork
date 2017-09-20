using System;
using System.ComponentModel.DataAnnotations;

namespace CourseWork.DataLayer.Models
{
    public class Payment
    {
        [Key]
        public string Id { get; set; }

        public string ProjectId { get; set; }

        public string UserName { get; set; }

        public DateTime Time { get; set; }

        public decimal PaidAmount { get; set; }

        public Project Project { get; set; }

        public UserInfo UserInfo { get; set; }
    }
}
