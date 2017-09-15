using System.Collections.Generic;
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

        public IEnumerable<Payment> GetProjectPayments(string projectId)
        {
            return _paymentRepository.GetWhere(payment => payment.ProjectId == projectId);
        }
    }
}
