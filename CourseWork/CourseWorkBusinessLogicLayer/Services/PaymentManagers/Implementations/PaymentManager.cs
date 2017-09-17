using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.PaymentViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.PaymentManagers.Implementations
{
    public class PaymentManager : IPaymentManager
    {
        private readonly Repository<Payment> _paymentRepository;
        private readonly Repository<Project> _projectRepository;
        private readonly Repository<UserInfo> _userInfoRepository;
        private readonly IMapper<PaymentFormViewModel, Payment> _paymentMapper;
        private readonly IUserManager _userManager;

        public PaymentManager(Repository<Payment> paymentRepository,
            IMapper<PaymentFormViewModel, Payment> paymentMapper, Repository<Project> projectRepository,
            Repository<UserInfo> userInfoRepository, IUserManager userManager)
        {
            _paymentRepository = paymentRepository;
            _paymentMapper = paymentMapper;
            _projectRepository = projectRepository;
            _userInfoRepository = userInfoRepository;
            _userManager = userManager;
        }

        public decimal GetProjectPaidAmount(string projectId, IEnumerable<Payment> payments)
        {
            var projectPayments = payments?.Where(payment => payment.ProjectId == projectId);
            return projectPayments?.Sum(payment => payment.PaidAmount) ?? 0;
        }

        public IEnumerable<Payment> GetProjectPayments(string projectId)
        {
            return _paymentRepository.GetWhere(payment => payment.ProjectId == projectId);
        }

        public bool AddPayment(PaymentFormViewModel paymentForm)
        {
            UpdateAccountNumber(paymentForm);
            var payment = _paymentMapper.ConvertTo(paymentForm);
            //changestatus
            return _paymentRepository.AddRange(payment);
        }

        public PaymentForFormViewModel GetPaymentInfoForForm(string projectId)
        {
            var project = _projectRepository.Get(projectId);
            return new PaymentForFormViewModel
            {
                KeptAccountNumber = _userManager.GetCurrentUserUserInfo().LastAccountNumber,
                MinPaymentAmount = project.MinPayment,
                MaxPaymentAmount = project.MaxPayment
            };
        }

        private void UpdateAccountNumber(PaymentFormViewModel paymentForm)
        {
            var userInfo = _userManager.GetCurrentUserUserInfo();
            userInfo.LastAccountNumber = paymentForm.AccountNumber;
            _userInfoRepository.UpdateRange(userInfo);
        }
    }
}
