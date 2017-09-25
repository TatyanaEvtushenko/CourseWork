using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.AwardViewModels;

namespace CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels
{
    public class UserSmallViewModel
    {
        public string UserName { get; set; }

        public string Avatar { get; set; }

        public IEnumerable<AwardSmallViewModel> Awards { get; set; }
    }
}
