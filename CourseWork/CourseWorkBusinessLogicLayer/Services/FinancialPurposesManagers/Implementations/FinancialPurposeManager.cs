using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.FinancialPurposesManagers.Implementations
{
    public class FinancialPurposeManager : IFinancialPurposeManager
    {
        private readonly IMapper<FinancialPurposeViewModel, FinancialPurpose> _financialPurposeMapper;

        public FinancialPurposeManager(IMapper<FinancialPurposeViewModel, FinancialPurpose> financialPurposeMapper)
        {
            _financialPurposeMapper = financialPurposeMapper;
        }

        public IEnumerable<FinancialPurposeViewModel> GetProjectFinancialPurposes(Project project, decimal paidAmount)
        {
            return project.FinancialPurposes.Select(p => GetFinancialPurposeViewModel(p, paidAmount));
        }

        public IEnumerable<FinancialPurposeViewModel> GetProjectFinancialPurposes(Project project)
        {
            var paidAmount = project.Payments.Sum(p => p.PaidAmount);
            return GetProjectFinancialPurposes(project, paidAmount);
        }

        public IEnumerable<FinancialPurpose> ConvertViewModelsToPurposes(IEnumerable<FinancialPurposeViewModel> purposes, string projectId)
        {
            return purposes.Select(p => GetNewFinancialPurpose(p, projectId));
        }

        public decimal GetProjectNeccessaryAmount(Project project)
        {
            return project.FinancialPurposes?.Sum(p => p.NecessaryPaymentAmount) ?? 0;
        }

        private FinancialPurposeViewModel GetFinancialPurposeViewModel(FinancialPurpose purpose, decimal paidAmount)
        {
            var purposeViewModel = _financialPurposeMapper.ConvertFrom(purpose);
            purposeViewModel.IsReached = purposeViewModel.Budget <= paidAmount;
            return purposeViewModel;
        }

        private FinancialPurpose GetNewFinancialPurpose(FinancialPurposeViewModel purpose, string projectId)
        {
            var purposeToAdding = _financialPurposeMapper.ConvertTo(purpose);
            purposeToAdding.ProjectId = projectId;
            return purposeToAdding;
        }
    }
}
