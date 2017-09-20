using System;
using CourseWork.BusinessLogicLayer.ViewModels.PaymentViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.ProjectMappers
{
    public class PaymentForFormViewModelToProjectMapper : IMapper<PaymentForFormViewModel, Project>
    {
        public Project ConvertTo(PaymentForFormViewModel item)
        {
            throw new NotImplementedException();
        }

        public PaymentForFormViewModel ConvertFrom(Project item)
        {
            return new PaymentForFormViewModel
            {
                MinPaymentAmount = item.MinPayment,
                MaxPaymentAmount = item.MaxPayment,
                ProjectName = item.Name,
                KeptAccountNumber = item.UserInfo.LastAccountNumber
            };
        }
    }
}
