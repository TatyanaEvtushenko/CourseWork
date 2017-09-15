using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;

namespace CourseWork.BusinessLogicLayer.Services.FinancialPurposeManagers
{
    public interface IFinancialPurposeManager
    {
        IEnumerable<FinancialPurposeViewModel> GetProjectFinancialPurposes(string projectId, decimal projectPaidAmount);
    }
}
