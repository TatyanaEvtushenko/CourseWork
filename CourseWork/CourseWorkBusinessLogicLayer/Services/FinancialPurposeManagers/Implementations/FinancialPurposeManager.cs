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

        public bool AddFinancialPurposes(IEnumerable<FinancialPurposeViewModel> financialPurposes, string projectId)
        {
            var purposes = financialPurposes.Select(purpose => GetPreparedFinancialPurpose(purpose, projectId)).ToArray();
            return _financialPurposeRepository.AddRange(purposes);
        }

        private FinancialPurpose GetPreparedFinancialPurpose(FinancialPurposeViewModel purpose, string projectId)
        {
            var purposeToAdding = _mapper.ConvertTo(purpose);
            purposeToAdding.Id = _financialPurposeRepository.GetNewId();
            purposeToAdding.PaidAmount = 0;
            purposeToAdding.ProjectId = projectId;
            return purposeToAdding;
        }
    }
}
