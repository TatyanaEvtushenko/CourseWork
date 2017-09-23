using System.Linq;
using CourseWork.BusinessLogicLayer.Services.AwardManagers;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.ProjectSubscriberManagers.Implementations
{
    public class ProjectSubscriberManager : IProjectSubscriberManager
    {
        private readonly IRepository<ProjectSubscriber> _projectSubscriberRepository;
        private readonly IUserManager _userManager;
        private readonly IAwardManager _awardManager;

        public ProjectSubscriberManager(IRepository<ProjectSubscriber> projectSubscriberRepository,
            IUserManager userManager, IAwardManager awardManager)
        {
            _projectSubscriberRepository = projectSubscriberRepository;
            _userManager = userManager;
            _awardManager = awardManager;
        }

        public bool Subscribe(string projectId, string awardName)
        {
            var subscriber = new ProjectSubscriber {ProjectId = projectId, UserName = _userManager.CurrentUserName};
            var result = _projectSubscriberRepository.AddRange(subscriber);
            if (result)
            {
                _awardManager.AddAwardForReceivedSubscriptions(projectId, awardName);
            }
            return result;
        }

        public bool Unsubscribe(string projectId)
        {
            return _projectSubscriberRepository.RemoveWhere(
                subscriber => subscriber.ProjectId == projectId && subscriber.UserName == _userManager.CurrentUserName);
        }

        public bool IsSubscriber(Project project)
        {
            return project.Subscribers?.FirstOrDefault(s => s.UserName == _userManager.CurrentUserName) != null;
        }

        public bool IsSubscriber(string projectId)
        {
            return _projectSubscriberRepository.FirstOrDefault(
                       s => s.UserName == _userManager.CurrentUserName && s.ProjectId == projectId) != null;
        }
    }
}