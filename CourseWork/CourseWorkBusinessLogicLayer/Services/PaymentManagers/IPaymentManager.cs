using System;

namespace CourseWork.BusinessLogicLayer.Services.PaymentManagers
{
    public interface IPaymentManager
    {
        DateTime GetTimeLastPayment(string projectId);
    }
}
