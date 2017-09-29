using System;
using CourseWork.BusinessLogicLayer.ViewModels.PaymentViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.PaymentMappers
{
    public class PaymentViewModelToPaymentMapper : IMapper<PaymentViewModel, Payment>
    {
        private readonly IMapper<UserSmallViewModel, UserInfo> _userInfoMapper;
        private readonly IMapper<ProjectSmallInfoViewModel, Project> _projectMapper;

        public PaymentViewModelToPaymentMapper(IMapper<UserSmallViewModel, UserInfo> userInfoMapper,
            IMapper<ProjectSmallInfoViewModel, Project> projectMapper)
        {
            _userInfoMapper = userInfoMapper;
            _projectMapper = projectMapper;
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
                ProjectInfo = _projectMapper.ConvertFrom(item.Project),
                Time = item.Time
            };
        }
    }
}
