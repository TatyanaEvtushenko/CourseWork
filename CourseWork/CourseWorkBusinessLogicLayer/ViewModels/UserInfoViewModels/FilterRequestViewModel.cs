using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels
{
    public class FilterRequestViewModel
    {
		public bool Confirmed { get; set; }

		public bool Requested { get; set; }

		public bool Unconfirmed { get; set; }
    }
}
