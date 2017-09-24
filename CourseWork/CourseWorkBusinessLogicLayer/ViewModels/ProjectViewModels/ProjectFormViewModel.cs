using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;

namespace CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels
{
    public class ProjectFormViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string FundRaisingEnd { get; set; }

        public string Description { get; set; }

        public string ImageBase64 { get; set; }

        public string AccountNumber { get; set; }

        public decimal MinPaymentAmount { get; set; }

        public decimal MaxPaymentAmount { get; set; }

        public IEnumerable<FinancialPurposeViewModel> FinancialPurposes { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
