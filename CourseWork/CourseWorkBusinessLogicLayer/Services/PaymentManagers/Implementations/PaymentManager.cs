using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.ViewModels.PaymentViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using CourseWork.DataLayer.Repositories.Implementations;

namespace CourseWork.BusinessLogicLayer.Services.PaymentManagers.Implementations
{
    public class PaymentManager : IPaymentManager
    {
        private readonly Repository<Payment> _paymentRepository;
        private readonly Repository<Project> _projectRepository;
        private readonly IMapper<PaymentForFormViewModel, Project> _paymentForFormMapper;
        private readonly IMapper<PaymentViewModel, Payment> _paymentMapper;

        public PaymentManager(Repository<Payment> paymentRepository, Repository<Project> projectRepository,
            IMapper<PaymentForFormViewModel, Project> paymentForFormMapper,
            IMapper<PaymentViewModel, Payment> paymentMapper)
        {
            _paymentRepository = paymentRepository;
            _projectRepository = projectRepository;
            _paymentForFormMapper = paymentForFormMapper;
            _paymentMapper = paymentMapper;
        }

        public IEnumerable<PaymentViewModel> GetBigPayments()
        {
            return _paymentRepository.GetOrdered(payment => payment.PaidAmount, 4, true, 
                payment => payment.Project, payment => payment.UserInfo)
                .Select(payment => _paymentMapper.ConvertFrom(payment));
        }

        public decimal GetProjectPaidAmount(Project project)
        {
            return project.Payments?.Sum(payment => payment.PaidAmount) ?? 0;
        }

        public PaymentForFormViewModel GetPaymentInfoForForm(string projectId)
        {
            var project = _projectRepository.FirstOrDefault(p => p.Id == projectId, p => p.UserInfo);
            return _paymentForFormMapper.ConvertFrom(project);
        }
    }
}
