using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.FinancialPurposeManagers.Implementations
{
    public class FinancialPurposeManager : IFinancialPurposeManager
    {
        private readonly Repository<FinancialPurpose> _financialPurposeRepository;
        private readonly IMapper<FinancialPurposeViewModel, FinancialPurpose> _mapper;

        public FinancialPurposeManager(Repository<FinancialPurpose> financialPurposeRepository,
            IMapper<FinancialPurposeViewModel, FinancialPurpose> mapper)
        {
            _financialPurposeRepository = financialPurposeRepository;
            _mapper = mapper;
        }

        public IEnumerable<FinancialPurposeViewModel> GetProjectFinancialPurposes(string projectId, decimal projectPaidAmount)
        {
            return _financialPurposeRepository.GetWhere(purpose => purpose.ProjectId == projectId).Select(purpose =>
            {
                var purposeViewModel = _mapper.ConvertFrom(purpose);
                purposeViewModel.IsReached = purposeViewModel.Budget <= projectPaidAmount;
                return purposeViewModel;
            });
        }
    }
}
