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
                NecessaryPaymentAmount = item.Budget,
                IsReached = item.IsReached
            };
        }

        public FinancialPurposeViewModel ConvertFrom(FinancialPurpose item)
        {
            return new FinancialPurposeViewModel()
            {
                Description = item.Description,
                Name = item.Name,
                Budget = item.NecessaryPaymentAmount,
                IsReached = item.IsReached
            };
        }
    }
}
