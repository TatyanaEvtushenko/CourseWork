using System.Collections.Generic;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.PaymentManagers
{
    public interface IPaymentManager
    {
        IEnumerable<Payment> GetProjectPayments(string projectId);
    }
}
