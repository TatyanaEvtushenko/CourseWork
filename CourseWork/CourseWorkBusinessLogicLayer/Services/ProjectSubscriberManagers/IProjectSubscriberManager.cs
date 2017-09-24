using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.ProjectSubscriberManagers
{
    public interface IProjectSubscriberManager
    {
        bool Subscribe(string projectId, string awardName);

        bool Unsubscribe(string projectId);

        bool IsSubscriber(Project project);

        bool IsSubscriber(string projectId);
    }
}
