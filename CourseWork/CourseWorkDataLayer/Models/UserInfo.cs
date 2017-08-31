﻿using System;
using CourseWork.DataLayer.Enums;

namespace CourseWork.DataLayer.Models
{
    public class UserInfo
    {
        public string UserId { get; set; }

        public UserStatus Status { get; set; }

        public bool IsBlocked { get; set; }

        public DateTime RegistrationTime { get; set; }

        public DateTime LastLoginTime { get; set; }

        public int ProjectNumber { get; set; }

        public int Raiting { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PassportScan { get; set; }

        public string Description { get; set; }
    }
}
