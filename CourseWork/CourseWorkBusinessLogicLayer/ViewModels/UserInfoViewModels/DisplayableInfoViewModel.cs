using System;
using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.AwardViewModels;

namespace CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels
{
    public class DisplayableInfoViewModel
    {
        public string UserName { get; set; }

        public DateTime RegistrationTime { get; set; }

        public int ProjectNumber { get; set; }

        public string Avatar { get; set; }

        public string About { get; set; }

        public string Contacts { get; set; }

        public IEnumerable<AwardViewModel> Awards { get; set; }

        public double Rating { get; set; }
    }
}
