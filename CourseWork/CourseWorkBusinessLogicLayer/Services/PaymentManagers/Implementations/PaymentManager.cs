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
        private readonly IMapper<PaymentForFormViewModel, Project> _paymentForFormMapper;
        private readonly IMapper<PaymentViewModel, Payment> _paymentMapper;
        private readonly IUserManager _userManager;

        public PaymentManager(Repository<Payment> paymentRepository, Repository<Project> projectRepository,
            IUserManager userManager,
            IMapper<PaymentForFormViewModel, Project> paymentForFormMapper, IMapper<PaymentViewModel, Payment> paymentMapper)
        {
            _paymentRepository = paymentRepository;
            _projectRepository = projectRepository;
            _userManager = userManager;
            _paymentForFormMapper = paymentForFormMapper;
            _paymentMapper = paymentMapper;
        }

        public IEnumerable<PaymentViewModel> GetBigPayments()
        {
            return _paymentRepository.GetOrdered(payment => payment.PaidAmount, 10, true, 
                payment => payment.Project, payment => payment.UserInfo)
                .Select(payment => _paymentMapper.ConvertFrom(payment));
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

        public PaymentForFormViewModel GetPaymentInfoForForm(string projectId)
        {
            var project = _projectRepository.Get(projectId);
            var paymentForForm = _paymentForFormMapper.ConvertFrom(project);
            paymentForForm.KeptAccountNumber = _userManager.GetCurrentUserUserInfo().LastAccountNumber;
            return paymentForForm;
        }
    }
}
