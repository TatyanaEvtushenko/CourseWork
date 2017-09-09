using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Services.AccountManagers;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.AdminManagers.Implementations
{
    public class AdminManager : IAdminManager
    {
        private readonly Repository<UserInfo> _userInfoRepository;
        private readonly Repository<ApplicationUser> _applicationUserRepository;
        private readonly Repository<Project> _projectRepository;
	    private readonly Repository<Raiting> _raitingRepository;
	    private readonly Repository<Comment> _commentRepository;
        private readonly IMapper<UserListItemViewModel, UserInfo> _mapperList;
        private readonly IMapper<UserConfirmationViewModel, UserInfo> _mapperInfo;
        private readonly IAccountManager _accountManager;

        public AdminManager(IMapper<UserListItemViewModel, UserInfo> mapperList, Repository<UserInfo> userInfoRepository, IMapper<UserConfirmationViewModel, UserInfo> mapperInfo, IAccountManager accountManager, Repository<ApplicationUser> applicationUserRepository, Repository<Project> projectRepository, Repository<Comment> commentRepository, Repository<Raiting> raitingRepository)
        {
            _mapperList = mapperList;
            _userInfoRepository = userInfoRepository;
            _mapperInfo = mapperInfo;
            _accountManager = accountManager;
            _applicationUserRepository = applicationUserRepository;
            _projectRepository = projectRepository;
	        _commentRepository = commentRepository;
	        _raitingRepository = raitingRepository;
        }

        public UserListItemViewModel[] GetAllUsers()
        {
            return _userInfoRepository.GetAll().Select(n => _mapperList.ConvertFrom(n)).ToArray();
        }

        public UserListItemViewModel[] GetFilteredUsers(bool confirmed, bool requested, bool unconfirmed)
        {
            return _userInfoRepository.GetWhere(item => (confirmed && item.Status == UserStatus.Confirmed) ||
                       (requested && item.Status == UserStatus.AwaitingConfirmation) ||
                       (unconfirmed && item.Status == UserStatus.WithoutConfirmation)
            ).Select(n => _mapperList.ConvertFrom(n)).ToArray();
        }

        public UserConfirmationViewModel GetPersonalInfo(string userName)
        {
            return _mapperInfo.ConvertFrom(_userInfoRepository.Get(userName));
        }

        public async Task<bool> RespondToConfirmation(string userName, bool accept)
        {
            var user = _userInfoRepository.Get(userName);
            user.Status = accept ? UserStatus.Confirmed : UserStatus.WithoutConfirmation;
            bool result = _userInfoRepository.UpdateRange(user);
            if (result && accept)
            {
                await _accountManager.RemoveRole(userName, UserRole.User);
                await _accountManager.AddRole(userName, UserRole.ConfirmedUser);
            }
            return result;
        }

        public UserListItemViewModel[] SortByField(string fieldName, bool ascending)
        {
            return _userInfoRepository.SortByField(fieldName, ascending).Select(n => _mapperList.ConvertFrom(n)).ToArray();
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
            return (!withCommentsAndRaitings ||
				(_raitingRepository.RemoveWhere(n => usersToDeleteSet.Contains(n.UserName)) &&
				_commentRepository.RemoveWhere(n => usersToDeleteSet.Contains(n.UserName)))) &&
				_projectRepository.RemoveWhere(n => usersToDeleteSet.Contains(n.OwnerUserName)) && 
                _userInfoRepository.RemoveRange(usersToDelete) &&
                _applicationUserRepository.RemoveWhere(n => usersToDeleteSet.Contains(n.UserName));
        }
    }
}
