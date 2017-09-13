using System;
using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;

namespace CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels
{
    public class ProjectViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int Rating { get; set; }

        public string Description { get; set; }

        public decimal PaidAmount { get; set; }

        public int CountOfPayments { get; set; }

        public IEnumerable<FinancialPurposeViewModel> FinancialPurposes { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public bool IsOwner { get; set; }


        public DateTime FundRaisingEnd { get; set; }

        public decimal MinPaymentAmount { get; set; }

        public decimal MaxPaymentAmount { get; set; }
    }
}
