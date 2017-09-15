using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.ProjectSubscriberManagers.Implementations
{
    public class ProjectSubscriberManager : IProjectSubscriberManager
    {
        private readonly Repository<ProjectSubscriber> _projectSubscriberRepository;
        private readonly IUserManager _userManager;

        public ProjectSubscriberManager(Repository<ProjectSubscriber> projectSubscriberRepository,
            IUserManager userManager)
        {
            _projectSubscriberRepository = projectSubscriberRepository;
            _userManager = userManager;
        }

        public IEnumerable<ProjectSubscriber> GetSubscribers(string projectId)
        {
            return _projectSubscriberRepository.GetWhere(subscriber => subscriber.ProjectId == projectId);
        }

        public bool Subscribe(string projectId)
        {
            var subscriber = new ProjectSubscriber {ProjectId = projectId, UserName = _userManager.CurrentUserName};
            return _projectSubscriberRepository.AddRange(subscriber);
        }

        public bool Unsubscribe(string projectId)
        {
            return _projectSubscriberRepository.RemoveWhere(
                subscriber => subscriber.ProjectId == projectId && subscriber.UserName == _userManager.CurrentUserName);
        }
    }
}