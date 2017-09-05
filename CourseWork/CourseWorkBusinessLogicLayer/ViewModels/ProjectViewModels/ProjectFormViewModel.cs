using System;
using System.Collections.Generic;

namespace CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels
{
    public class ProjectFormViewModel
    {
        public string Name { get; set; }

        public DateTime FundRaisingEnd { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<string> FinancialPurposeIdentificators { get; set; }

        public IEnumerable<string> TagIdentificators { get; set; }
    }
}
