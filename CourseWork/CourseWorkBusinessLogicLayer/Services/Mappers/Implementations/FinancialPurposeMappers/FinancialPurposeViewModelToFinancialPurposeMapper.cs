using System;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.FinancialPurposeMappers
{
    public class FinancialPurposeViewModelToFinancialPurposeMapper : IMapper<FinancialPurposeViewModel, FinancialPurpose>
    {
        public FinancialPurpose ConvertTo(FinancialPurposeViewModel item)
        {
            return new FinancialPurpose
            {
                Description = item.Description,
                Name = item.Name,
                NecessaryPaymentAmount = item.Budget,
                Id = item.Id ?? Guid.NewGuid().ToString()
            };
        }

        public FinancialPurposeViewModel ConvertFrom(FinancialPurpose item)
        {
            return new FinancialPurposeViewModel
            {
                Description = item.Description,
                Name = item.Name,
                Budget = item.NecessaryPaymentAmount,
                Id = item.Id,
            };
        }
    }
}
