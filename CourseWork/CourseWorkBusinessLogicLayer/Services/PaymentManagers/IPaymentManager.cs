using System.Collections.Generic;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.PaymentManagers
{
    public interface IPaymentManager
    {
        decimal GetProjectPaidAmount(string projectId, IEnumerable<Payment> payments);

        IEnumerable<Payment> GetProjectPayments(string projectId);
    }
}
