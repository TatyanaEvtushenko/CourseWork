using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels
{
    public class UserListItemViewModel
    {
        public string Status { get; set; }

        public string RegistrationTime { get; set; }

        public string LastLoginTime { get; set; }

        public string ProjectNumber { get; set; }

        public string Raiting { get; set; }
    }
}
