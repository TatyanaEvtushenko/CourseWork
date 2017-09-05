using System;
using System.ComponentModel.DataAnnotations;
using CourseWork.DataLayer.Enums;

namespace CourseWork.DataLayer.Models
{
    public class Project
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatingTime { get; set; }

        public DateTime FundRaisingEnd { get; set; }

        public ProjectStatus Status { get; set; }

        public string OwnerId { get; set; }

        public decimal MinPayment { get; set; }

        public decimal MaxPayment { get; set; }
    }
}
