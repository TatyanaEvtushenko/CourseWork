using System;
using CourseWork.BusinessLogicLayer.ViewModels.PaymentViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class PaymentViewModelToPaymentMapper : IMapper<PaymentViewModel, Payment>
    {
        private readonly IMapper<UserSmallViewModel, UserInfo> _userInfoMapper;

        public PaymentViewModelToPaymentMapper(IMapper<UserSmallViewModel, UserInfo> userInfoMapper)
        {
            _userInfoMapper = userInfoMapper;
        }

        public Payment ConvertTo(PaymentViewModel item)
        {
            throw new NotImplementedException();
        }

        public PaymentViewModel ConvertFrom(Payment item)
        {
            return new PaymentViewModel
            {
                PaidAmount = item.PaidAmount,
                Payer = _userInfoMapper.ConvertFrom(item.UserInfo),
                ProjectName = item.Project.Name,
                Time = item.Time
            };
        }
    }
}
