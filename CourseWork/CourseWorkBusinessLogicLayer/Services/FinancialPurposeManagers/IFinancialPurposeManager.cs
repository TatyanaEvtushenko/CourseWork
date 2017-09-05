using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;

namespace CourseWork.BusinessLogicLayer.Services.FinancialPurposeManagers
{
    public interface IFinancialPurposeManager
    {
        bool AddFinancialPurposes(IEnumerable<FinancialPurposeViewModel> financialPurposes, string projectId);
    }
}
