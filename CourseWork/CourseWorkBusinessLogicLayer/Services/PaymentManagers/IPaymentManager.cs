using System;
using System.Collections.Generic;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.PaymentManagers
{
    public interface IPaymentManager
    {
        DateTime GetTimeLastPayment(string projectId);

        IEnumerable<Payment> GetProjectPayments(string projectId);
    }
}
