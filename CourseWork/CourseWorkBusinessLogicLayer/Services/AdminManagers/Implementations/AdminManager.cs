using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Services.AccountManagers;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.SearchManagers;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using CourseWork.DataLayer.Repositories.Implementations;

namespace CourseWork.BusinessLogicLayer.Services.AdminManagers.Implementations
{
    public class AdminManager : IAdminManager
    {
        private readonly Repository<UserInfo> _userInfoRepository;
        private readonly Repository<Project> _projectRepository;
	    private readonly Repository<Rating> _raitingRepository;
	    private readonly Repository<Comment> _commentRepository;
        private readonly IMapper<UserListItemViewModel, UserInfo> _mapperList;
        private readonly IMapper<UserConfirmationViewModel, UserInfo> _mapperInfo;
        private readonly IAccountManager _accountManager;
        private readonly ISearchManager _searchManager;

        public AdminManager(IMapper<UserListItemViewModel, UserInfo> mapperList,
            Repository<UserInfo> userInfoRepository, IMapper<UserConfirmationViewModel, UserInfo> mapperInfo,
            IAccountManager accountManager,
            Repository<Project> projectRepository, Repository<Comment> commentRepository,
            Repository<Rating> raitingRepository, ISearchManager searchManager)
        {
            _mapperList = mapperList;
            _userInfoRepository = userInfoRepository;
            _mapperInfo = mapperInfo;
            _accountManager = accountManager;
            _projectRepository = projectRepository;
            _commentRepository = commentRepository;
            _raitingRepository = raitingRepository;
            _searchManager = searchManager;
        }

        public UserListItemViewModel[] GetAllUsers()
        {
            return _userInfoRepository.GetWhere(item => true, item => item.Projects).Select(item => _mapperList.ConvertFrom(item)).ToArray();
        }

        public UserListItemViewModel[] GetFilteredUsers(FilterRequestViewModel model)
        {
            return _userInfoRepository.GetWhere(GetFilterRequest(model), item => item.Projects)
                       .Select(item => _mapperList.ConvertFrom(item)).ToArray();
        }

        private Func<UserInfo, bool> GetFilterRequest(FilterRequestViewModel model)
        {
            return item => (model.Confirmed && item.Status == UserStatus.Confirmed) ||
                           (model.Requested && item.Status == UserStatus.AwaitingConfirmation) ||
                           (model.Unconfirmed && item.Status == UserStatus.WithoutConfirmation);
        }

        public UserConfirmationViewModel GetPersonalInfo(string userName)
        {
            var userInfo = _userInfoRepository.FirstOrDefault(x => x.UserName == userName);
            return _mapperInfo.ConvertFrom(userInfo);
        }

        public async Task<bool> RespondToConfirmation(string userName, bool accept)
        {
            var user = _userInfoRepository.FirstOrDefault(u => u.UserName == userName);
            user.Status = accept ? UserStatus.Confirmed : UserStatus.WithoutConfirmation;
            var result = _userInfoRepository.UpdateRange(user);
            if (!result || !accept)
            {
                return result;
            }
            await UpdateRole(userName, UserRole.User, UserRole.ConfirmedUser);
            return true;
        }

        public UserListItemViewModel[] SortByField(string fieldName, bool ascending, FilterRequestViewModel filters)
        {
            var filterRequest = GetFilterRequest(filters);
            return ((UserInfoRepository)_userInfoRepository).SortByField(fieldName, ascending, filterRequest).Select(n => _mapperList.ConvertFrom(n)).ToArray();
        }

        public bool BlockUnblock(IEnumerable<string> usersToBlock)
        {
            var users = _userInfoRepository.GetWhere(n => usersToBlock.Contains(n.UserName));
            foreach (var user in users)
            {
                user.IsBlocked = !user.IsBlocked;
            }
            return _userInfoRepository.UpdateRange(users.ToArray());
        }

        public bool Delete(IEnumerable<string> usersToDelete, bool withCommentsAndRaitings)
        {
            return withCommentsAndRaitings
                ? DeleteUsersWithCommentsndRatings(usersToDelete)
                : DeleteUsersWithoutCommentsndRatings(usersToDelete);
        }

        private async Task UpdateRole(string userName, UserRole oldRole, UserRole newRole)
        {
            await _accountManager.RemoveRole(userName, oldRole);
            await _accountManager.AddRole(userName, newRole);
        }

        private bool DeleteUsersWithoutCommentsndRatings(IEnumerable<string> usersToDelete)
        {
            var projectsToRemove = _projectRepository.GetWhere(n => usersToDelete.Contains(n.OwnerUserName)).ToArray();
            var result = _userInfoRepository.RemoveRange(usersToDelete);
            if (result)
            {
                RemoveIndexes(projectsToRemove, null);
            }
            return result;
        }

        private bool DeleteUsersWithCommentsndRatings(IEnumerable<string> usersToDelete)
        {
            var commentsToRemove = _commentRepository.GetWhere(n => usersToDelete.Contains(n.UserName));
            var projectsToRemove = _projectRepository.GetWhere(n => usersToDelete.Contains(n.OwnerUserName));
            var result = RemoveWithCommentsAndRatings(usersToDelete);
            if (result)
            {
                RemoveIndexes(projectsToRemove.ToArray(), commentsToRemove.ToArray());
            }
            return result;
        }

        private bool RemoveWithCommentsAndRatings(IEnumerable<string> usersToDelete)
        {
            return _raitingRepository.RemoveWhere(n => usersToDelete.Contains(n.UserName)) &&
                   _commentRepository.RemoveWhere(n => usersToDelete.Contains(n.UserName)) &&
                   _userInfoRepository.RemoveRange(usersToDelete);

        }

        private void RemoveIndexes(Project[] projectsToRemove, Comment[] commentsToRemove)
        {
            _searchManager.RemoveCommentsFromIndex(commentsToRemove);
            _searchManager.RemoveProjectsFromIndex(projectsToRemove);
        }
    }
}
