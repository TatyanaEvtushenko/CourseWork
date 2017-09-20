using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.PaymentViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.PaymentManagers
{
    public interface IPaymentManager
    {
        PaymentForFormViewModel GetPaymentInfoForForm(string projectId);

        IEnumerable<PaymentViewModel> GetBigPayments();

        decimal GetProjectPaidAmount(Project project);
    }
}
