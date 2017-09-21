using System;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.PaymentViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.PaymentMappers
{
    public class PaymentFormViewModelToPaymentMapper : IMapper<PaymentFormViewModel, Payment>
    {
        private readonly IUserManager _userManager;

        public PaymentFormViewModelToPaymentMapper(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public Payment ConvertTo(PaymentFormViewModel item)
        {
            return new Payment
            {
                Id = Guid.NewGuid().ToString(),
                PaidAmount = item.PaidAmount,
                ProjectId = item.ProjectId,
                Time = DateTime.UtcNow,
                UserName = _userManager.CurrentUserName
            };
        }

        public PaymentFormViewModel ConvertFrom(Payment item)
        {
            throw new NotImplementedException();
        }
    }
}
