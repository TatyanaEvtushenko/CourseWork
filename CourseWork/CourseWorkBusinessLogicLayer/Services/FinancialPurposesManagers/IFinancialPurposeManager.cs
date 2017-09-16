using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;

namespace CourseWork.BusinessLogicLayer.Services.FinancialPurposesManagers
{
    public interface IFinancialPurposeManager
    {
        IEnumerable<FinancialPurposeViewModel> GetProjectFinancialPurposeViewModels(string projectId);

        IEnumerable<FinancialPurposeViewModel> GetProjectFinancialPurposeViewModels(string projectId,
            decimal paidAmount);
    }
}
