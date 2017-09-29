using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.Services.PaymentManagers;
using CourseWork.BusinessLogicLayer.Services.ProjectManagers;
using CourseWork.BusinessLogicLayer.ViewModels.PaymentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IPaymentManager _paymentManager;
        private readonly IProjectManager _projectManager;

        public PaymentController(IPaymentManager paymentManager, IProjectManager projectManager)
        {
            _paymentManager = paymentManager;
            _projectManager = projectManager;
        }

        [HttpGet]
        [Route("api/Payment/GetBigPayments")]
        [AllowAnonymous]
        public IEnumerable<PaymentViewModel> GetBigPayments()
        {
            return _paymentManager.GetBigPayments();
        }

        [HttpGet]
        [Route("api/Payment/GetPaymentInfoForForm/{projectId}")]
        public PaymentForFormViewModel GetPaymentInfoForForm(string projectId)
        {
            return _paymentManager.GetPaymentInfoForForm(projectId);
        }

        [HttpPost]
        [Route("api/Payment/AddPayment")]
        public bool AddPayment([FromBody]PaymentFormViewModel payment)
        {
            return _projectManager.AddPayment(payment);
        }
    }
}