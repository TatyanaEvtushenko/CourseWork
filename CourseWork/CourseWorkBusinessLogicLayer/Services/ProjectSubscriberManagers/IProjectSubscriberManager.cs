using System.Collections.Generic;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.ProjectSubscriberManagers
{
    public interface IProjectSubscriberManager
    {
        IEnumerable<ProjectSubscriber> GetSubscribers(string projectId);

        bool Subscribe(string projectId);

        bool Unsubscribe(string projectId);
    }
}
