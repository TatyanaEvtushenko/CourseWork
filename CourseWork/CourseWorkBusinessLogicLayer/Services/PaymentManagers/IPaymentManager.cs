using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.PaymentViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.PaymentManagers
{
    public interface IPaymentManager
    {
        decimal GetProjectPaidAmount(string projectId, IEnumerable<Payment> payments);

        IEnumerable<Payment> GetProjectPayments(string projectId);

        bool AddPayment(PaymentFormViewModel paymentForm);

        PaymentForFormViewModel GetPaymentInfoForForm(string projectId);
    }
}
