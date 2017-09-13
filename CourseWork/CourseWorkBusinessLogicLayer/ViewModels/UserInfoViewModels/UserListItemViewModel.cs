﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels
{
    public class UserListItemViewModel
    {
        public string Username { get; set; }

        public string Status { get; set; }

        public string RegistrationTime { get; set; }

        public string LastLoginTime { get; set; }

        public string ProjectNumber { get; set; }

        public string Raiting { get; set; }

        public int StatusCode { get; set; }

        public bool IsBlocked { get; set; }
    }
}