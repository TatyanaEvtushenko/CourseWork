using System.Collections.Generic;
using System.Linq;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.PaymentManagers.Implementations
{
    public class PaymentManager : IPaymentManager
    {
        private readonly Repository<Payment> _paymentRepository;

        public PaymentManager(Repository<Payment> paymentRepository)
        {
            _paymentRepository = paymentRepository;
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
    }
}
