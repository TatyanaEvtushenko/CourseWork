using System;
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

        public ProjectSubscriberManager(Repository<ProjectSubscriber> projectSubscriberRepository, IUserManager userManager)
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
            if (GetProjectSubscriber(projectId, _userManager.CurrentUserName) != null)
            {
                return false;
            }
            var subscriber = GetPreparedProjectSubscriber(projectId, _userManager.CurrentUserName);
            return _projectSubscriberRepository.AddRange(subscriber);
        }

        public bool Unsubscribe(string projectId)
        {
            var subscriber = GetProjectSubscriber(projectId, _userManager.CurrentUserName);
            return subscriber != null && _projectSubscriberRepository.RemoveRange(subscriber.Id);
        }

        private ProjectSubscriber GetProjectSubscriber(string projectId, string userName)
        {
            return _projectSubscriberRepository.FirstOrDefault(
                subscriber => subscriber.ProjectId == projectId && subscriber.UserName == userName);
        }

        private ProjectSubscriber GetPreparedProjectSubscriber(string projectId, string userName)
        {
            return new ProjectSubscriber
            {
                Id = Guid.NewGuid().ToString(),
                ProjectId = projectId,
                UserName = userName
            };
        }
    }
}
