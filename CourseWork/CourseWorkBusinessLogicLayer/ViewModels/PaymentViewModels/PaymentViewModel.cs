using System;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;

namespace CourseWork.BusinessLogicLayer.ViewModels.PaymentViewModels
{
    public class PaymentViewModel
    {
        public UserSmallViewModel Payer { get; set; }

        public string ProjectName { get; set; }

        public DateTime Time { get; set; }

        public decimal PaidAmount { get; set; }
    }
}
