using CourseWork.BusinessLogicLayer.Services.PaymentManagers;
using CourseWork.BusinessLogicLayer.ViewModels.PaymentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IPaymentManager _paymentManager;

        public PaymentController(IPaymentManager paymentManager)
        {
            _paymentManager = paymentManager;
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
            return _paymentManager.AddPayment(payment);
        }
    }
}