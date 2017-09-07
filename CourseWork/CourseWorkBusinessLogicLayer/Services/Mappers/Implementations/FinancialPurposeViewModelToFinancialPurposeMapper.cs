using System;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class FinancialPurposeViewModelToFinancialPurposeMapper : IMapper<FinancialPurposeViewModel, FinancialPurpose>
    {
        public FinancialPurpose ConvertTo(FinancialPurposeViewModel item)
        {
            return new FinancialPurpose
            {
                Description = item.Description,
                Name = item.Name,
                NecessaryPaymentAmount = item.Budget
            };
        }

        public FinancialPurposeViewModel ConvertFrom(FinancialPurpose item)
        {
            throw new NotImplementedException();
        }
    }
}
