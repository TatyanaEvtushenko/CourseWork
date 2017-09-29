using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Options;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.ViewModels.PaymentViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Microsoft.Extensions.Options;

namespace CourseWork.BusinessLogicLayer.Services.PaymentManagers.Implementations
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IMapper<PaymentForFormViewModel, Project> _paymentForFormMapper;
        private readonly IMapper<PaymentViewModel, Payment> _paymentMapper;
        private readonly HomePageOptions _options;

        public PaymentManager(IRepository<Payment> paymentRepository, IRepository<Project> projectRepository,
            IMapper<PaymentForFormViewModel, Project> paymentForFormMapper,
            IMapper<PaymentViewModel, Payment> paymentMapper, IOptions<HomePageOptions> options)
        {
            _paymentRepository = paymentRepository;
            _projectRepository = projectRepository;
            _paymentForFormMapper = paymentForFormMapper;
            _paymentMapper = paymentMapper;
            _options = options.Value;
        }

        public IEnumerable<PaymentViewModel> GetBigPayments()
        {
            return _paymentRepository.GetOrdered(payment => payment.PaidAmount, _options.BiggestPaymentsCount, true, 
                payment => payment.Project, payment => payment.UserInfo, payment => payment.UserInfo.Awards)
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
