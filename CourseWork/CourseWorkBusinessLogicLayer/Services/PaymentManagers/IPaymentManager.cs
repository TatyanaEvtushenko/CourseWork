﻿using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.PaymentViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.PaymentManagers
{
    public interface IPaymentManager
    {
        decimal GetProjectPaidAmount(string projectId, IEnumerable<Payment> payments);

        IEnumerable<Payment> GetProjectPayments(string projectId);

        PaymentForFormViewModel GetPaymentInfoForForm(string projectId);

        IEnumerable<PaymentViewModel> GetBigPayments();
    }
}
