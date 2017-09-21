using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.FinancialPurposesManagers
{
    public interface IFinancialPurposeManager
    {
        IEnumerable<FinancialPurposeViewModel> GetProjectFinancialPurposes(Project project, decimal paidAmount);

        IEnumerable<FinancialPurposeViewModel> GetProjectFinancialPurposes(Project project);

        IEnumerable<FinancialPurpose> ConvertViewModelsToPurposes(IEnumerable<FinancialPurposeViewModel> purposes,
            string projectId);

        decimal GetProjectNeccessaryAmount(Project project);
    }
}
