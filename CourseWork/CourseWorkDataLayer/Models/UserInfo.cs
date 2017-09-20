using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CourseWork.DataLayer.Enums;

namespace CourseWork.DataLayer.Models
{
    public class UserInfo
    {
        [Key]
        public string UserName { get; set; }

        public UserStatus Status { get; set; }

        public bool IsBlocked { get; set; }

        public DateTime RegistrationTime { get; set; }

        public DateTime LastLoginTime { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PassportScan { get; set; }

        public string Description { get; set; }

        public string Avatar { get; set; }

        public string About { get; set; }

        public string Contacts { get; set; }

        public string LastAccountNumber { get; set; }

      //  public ApplicationUser ApplicationUser { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<Message> Messages { get; set; }

        public IEnumerable<Payment> Payments { get; set; }

        public IEnumerable<ProjectSubscriber> Subscriptions { get; set; }

        public IEnumerable<Project> Projects { get; set; }

        public IEnumerable<Rating> Ratings { get; set; }
    }
}
