using System;
using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;

namespace CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels
{
    public class ProjectFormViewModel
    {
        public string Name { get; set; }

        public DateTime FundRaisingEnd { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal MinPaymentAmount { get; set; }

        public decimal MaxPaymentAmount { get; set; }

        public IEnumerable<FinancialPurposeViewModel> FinancialPurposes { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
