using System.Collections.Generic;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.ProjectSubscriberManager.Implementations
{
    public class ProjectSubscriberManager : IProjectSubscriberManager
    {
        private readonly Repository<ProjectSubscriber> _projectSubscriberRepository;

        public ProjectSubscriberManager(Repository<ProjectSubscriber> projectSubscriberRepository)
        {
            _projectSubscriberRepository = projectSubscriberRepository;
        }

        public IEnumerable<ProjectSubscriber> GetSubscribers(string projectId)
        {
            return _projectSubscriberRepository.GetWhere(subscriber => subscriber.ProjectId == projectId);
        }
    }
}
