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
using CourseWork.DataLayer.Repositories.Implementations;

namespace CourseWork.BusinessLogicLayer.Services.AdminManagers.Implementations
{
    public class AdminManager : IAdminManager
    {
        private readonly Repository<UserInfo> _userInfoRepository;
        private readonly Repository<ApplicationUser> _applicationUserRepository;
        private readonly Repository<Project> _projectRepository;
	    private readonly Repository<Rating> _raitingRepository;
	    private readonly Repository<Comment> _commentRepository;
        private readonly IMapper<UserListItemViewModel, UserInfo> _mapperList;
        private readonly IMapper<UserConfirmationViewModel, UserInfo> _mapperInfo;
        private readonly IAccountManager _accountManager;
        private readonly ISearchManager _searchManager;

        public AdminManager(IMapper<UserListItemViewModel, UserInfo> mapperList,
            Repository<UserInfo> userInfoRepository, IMapper<UserConfirmationViewModel, UserInfo> mapperInfo,
            IAccountManager accountManager, Repository<ApplicationUser> applicationUserRepository,
            Repository<Project> projectRepository, Repository<Comment> commentRepository,
            Repository<Rating> raitingRepository, ISearchManager searchManager)
        {
            _mapperList = mapperList;
            _userInfoRepository = userInfoRepository;
            _mapperInfo = mapperInfo;
            _accountManager = accountManager;
            _applicationUserRepository = applicationUserRepository;
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
            var userInfo = _userInfoRepository.Get( userName);
            return _mapperInfo.ConvertFrom(userInfo);
        }

        public async Task<bool> RespondToConfirmation(string userName, bool accept)
        {
            var user = _userInfoRepository.Get(userName);
            user.Status = accept ? UserStatus.Confirmed : UserStatus.WithoutConfirmation;
            var result = _userInfoRepository.UpdateRange(user);
            if (!result || !accept)
            {
                return result;
            }
            await _accountManager.RemoveRole(userName, UserRole.User);
            await _accountManager.AddRole(userName, UserRole.ConfirmedUser);
            return true;
        }

        public UserListItemViewModel[] SortByField(string fieldName, bool ascending, FilterRequestViewModel filters)
        {
            var filterRequest = GetFilterRequest(filters);
            return ((UserInfoRepository)_userInfoRepository).SortByField(fieldName, ascending, filterRequest).Select(n => _mapperList.ConvertFrom(n)).ToArray();
        }

        public bool BlockUnblock(string[] usersToBlock)
        {
            var users = _userInfoRepository.GetWhere(n => usersToBlock.Contains(n.UserName));
            foreach (var user in users)
            {
                user.IsBlocked = !user.IsBlocked;
            }
            return _userInfoRepository.UpdateRange(users.ToArray());
        }

        public bool Delete(string[] usersToDelete, bool withCommentsAndRaitings)
        {
	        var usersToDeleteSet = usersToDelete.ToImmutableHashSet();
            var projectsToRemove = _projectRepository.GetWhere(n => usersToDeleteSet.Contains(n.OwnerUserName));
            Comment[] commentsToRemove = null;
            if (withCommentsAndRaitings)
                commentsToRemove = _commentRepository.GetWhere(item => usersToDelete.Contains(item.UserName)).ToArray();
            return (!withCommentsAndRaitings ||
				(_raitingRepository.RemoveWhere(n => usersToDeleteSet.Contains(n.UserName)) &&
				_commentRepository.RemoveWhere(n => usersToDeleteSet.Contains(n.UserName)) &&
                _searchManager.RemoveCommentsFromIndex(commentsToRemove))) &&
                 _searchManager.RemoveProjectsFromIndex(projectsToRemove.ToArray()) && 
                _userInfoRepository.RemoveRange(usersToDelete) &&
                _applicationUserRepository.RemoveWhere(n => usersToDeleteSet.Contains(n.UserName));
        }
    }
}
