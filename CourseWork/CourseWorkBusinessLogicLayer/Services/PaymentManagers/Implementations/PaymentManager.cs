using System;
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

        public DateTime GetTimeLastPayment(string projectId)
        {
            var lastPayment = _paymentRepository.GetWhere(payment => payment.ProjectId == projectId)
                .OrderByDescending(payment => payment.Time).FirstOrDefault();
            return lastPayment?.Time ?? DateTime.Now;
        }
    }
}
