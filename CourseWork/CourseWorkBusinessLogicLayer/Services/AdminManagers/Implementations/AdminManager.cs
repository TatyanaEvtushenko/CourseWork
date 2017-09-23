using System.Collections.Generic;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Services.AccountManagers;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.SearchManagers;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using CourseWork.DataLayer.Repositories.Implementations.Implementations;

namespace CourseWork.BusinessLogicLayer.Services.AdminManagers.Implementations
{
    public class AdminManager : IAdminManager
    {
        private readonly IRepository<UserInfo> _userInfoRepository;
        private readonly IRepository<Project> _projectRepository;
	    private readonly IRepository<Rating> _raitingRepository;
	    private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<ApplicationUser> _applicationUserRepository;
        private readonly IMapper<UserListItemViewModel, UserInfo> _mapperList;
        private readonly IMapper<UserConfirmationViewModel, UserInfo> _mapperInfo;
        private readonly IAccountManager _accountManager;
        private readonly ISearchManager _searchManager;

        public AdminManager(IMapper<UserListItemViewModel, UserInfo> mapperList,
            IRepository<UserInfo> userInfoRepository, IMapper<UserConfirmationViewModel, UserInfo> mapperInfo,
            IAccountManager accountManager, IRepository<Project> projectRepository,
            IRepository<Comment> commentRepository, IRepository<Rating> raitingRepository, ISearchManager searchManager, IRepository<ApplicationUser> applicationUserRepository)
        {
            _mapperList = mapperList;
            _userInfoRepository = userInfoRepository;
            _mapperInfo = mapperInfo;
            _accountManager = accountManager;
            _projectRepository = projectRepository;
            _commentRepository = commentRepository;
            _raitingRepository = raitingRepository;
            _searchManager = searchManager;
            _applicationUserRepository = applicationUserRepository;
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
            var result = _projectRepository.RemoveWhere(n => usersToDelete.Contains(n.OwnerUserName)) &&
                _userInfoRepository.RemoveRange(usersToDelete.ToArray()) &&
                 _applicationUserRepository.RemoveWhere(n => usersToDelete.Contains(n.UserName));
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
            var result = _projectRepository.RemoveWhere(n => usersToDelete.Contains(n.OwnerUserName)) && RemoveWithCommentsAndRatings(usersToDelete);
            if (result)
            {
                RemoveIndexes(projectsToRemove.ToArray(), commentsToRemove.ToArray());
            }
            return result;
        }

        private bool RemoveWithCommentsAndRatings(IEnumerable<string> usersToDelete)
        {
            var usersToDeleteArray = usersToDelete.ToArray();
            return _raitingRepository.RemoveWhere(n => usersToDeleteArray.Contains(n.UserName)) &&
                   _commentRepository.RemoveWhere(n => usersToDeleteArray.Contains(n.UserName)) &&
                   _userInfoRepository.RemoveRange(usersToDeleteArray) &&
                   _applicationUserRepository.RemoveWhere(n => usersToDeleteArray.Contains(n.UserName));

        }

        private void RemoveIndexes(Project[] projectsToRemove, Comment[] commentsToRemove)
        {
            _searchManager.RemoveCommentsFromIndex(commentsToRemove);
            _searchManager.RemoveProjectsFromIndex(projectsToRemove);
        }
    }
}
