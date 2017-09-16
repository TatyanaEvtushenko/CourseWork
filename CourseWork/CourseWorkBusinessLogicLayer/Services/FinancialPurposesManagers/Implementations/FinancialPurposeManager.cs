using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.PaymentManagers;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.FinancialPurposesManagers.Implementations
{
    public class FinancialPurposeManager : IFinancialPurposeManager
    {
        private readonly IPaymentManager _paymentManager;
        private readonly Repository<FinancialPurpose> _financialPurposeRepository;
        private readonly IMapper<FinancialPurposeViewModel, FinancialPurpose> _financialPurposeMapper;

        public FinancialPurposeManager(Repository<FinancialPurpose> financialPurposeRepository,
            IPaymentManager paymentManager, IMapper<FinancialPurposeViewModel, FinancialPurpose> financialPurposeMapper)
        {
            _financialPurposeRepository = financialPurposeRepository;
            _paymentManager = paymentManager;
            _financialPurposeMapper = financialPurposeMapper;
        }

        public IEnumerable<FinancialPurposeViewModel> GetProjectFinancialPurposeViewModels(string projectId)
        {
            var projectPayments = _paymentManager.GetProjectPayments(projectId);
            var projectPaidAmount = _paymentManager.GetProjectPaidAmount(projectId, projectPayments);
            return GetProjectFinancialPurposeViewModels(projectId, projectPaidAmount);
        }

        public IEnumerable<FinancialPurposeViewModel> GetProjectFinancialPurposeViewModels(string projectId, decimal paidAmount)
        {
            return _financialPurposeRepository.GetWhere(purpose => purpose.ProjectId == projectId).Select(purpose =>
            {
                var purposeViewModel = _financialPurposeMapper.ConvertFrom(purpose);
                purposeViewModel.IsReached = purposeViewModel.Budget <= paidAmount;
                return purposeViewModel;
            });
        }
    }
}
