using CourseWork.BusinessLogicLayer.Services.ProjectSubscriberManagers;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    public class SubscriberController : Controller
    {
        private readonly IProjectSubscriberManager _projectSubscriberManager;

        public SubscriberController(IProjectSubscriberManager projectSubscriberManager)
        {
            _projectSubscriberManager = projectSubscriberManager;
        }

        [HttpPost]
        [Route("api/Subscriber/Subscribe")]
        public bool Subscribe([FromBody]string projectId)
        {
            return _projectSubscriberManager.Subscribe(projectId);
        }

        [HttpPost]
        [Route("api/Subscriber/Unsubscribe")]
        public bool Unsubscribe([FromBody]string projectId)
        {
            return _projectSubscriberManager.Unsubscribe(projectId);
        }
    }
}