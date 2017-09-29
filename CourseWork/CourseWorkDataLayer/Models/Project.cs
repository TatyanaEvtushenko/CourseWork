using System;
using System.Collections.Generic;
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

        public string OwnerUserName { get; set; }

        public decimal MinPayment { get; set; }

        public decimal MaxPayment { get; set; }

        public string Color { get; set; }

        public string AccountNumber { get; set; }

        public UserInfo UserInfo { get; set; }

        public IEnumerable<ProjectSubscriber> Subscribers { get; set; }

        public IEnumerable<Payment> Payments { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<FinancialPurpose> FinancialPurposes { get; set; }

        public IEnumerable<News> News { get; set; }

        public IEnumerable<Rating> Ratings { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
