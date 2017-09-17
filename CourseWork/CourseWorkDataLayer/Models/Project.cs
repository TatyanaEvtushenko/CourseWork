using System;
using System.ComponentModel.DataAnnotations;
using CourseWork.DataLayer.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string OwnerUserName { get; set; }

        public decimal MinPayment { get; set; }

        public decimal MaxPayment { get; set; }

        public decimal PaidAmount { get; set; }

        public double Raiting { get; set; }

        [ForeignKey("OwnerUserName")]
        [Required]
        public UserInfo UserInfo  { get; set; }
    }
}
