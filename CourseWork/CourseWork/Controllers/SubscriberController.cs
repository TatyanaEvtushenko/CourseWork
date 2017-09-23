using CourseWork.BusinessLogicLayer.Services.ProjectSubscriberManagers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class SubscriberController : Controller
    {
        private readonly IProjectSubscriberManager _projectSubscriberManager;
        private readonly IStringLocalizer<LocalizationController> _localizer;

        public SubscriberController(IProjectSubscriberManager projectSubscriberManager, IStringLocalizer<LocalizationController> localizer)
        {
            _projectSubscriberManager = projectSubscriberManager;
            _localizer = localizer;
        }

        [HttpPost]
        [Route("api/Subscriber/Subscribe")]
        public bool Subscribe([FromBody]string projectId)
        {
            return _projectSubscriberManager.Subscribe(projectId, _localizer["PEOPLEPERSON"]);
        }

        [HttpPost]
        [Route("api/Subscriber/Unsubscribe")]
        public bool Unsubscribe([FromBody]string projectId)
        {
            return _projectSubscriberManager.Unsubscribe(projectId);
        }
    }
}