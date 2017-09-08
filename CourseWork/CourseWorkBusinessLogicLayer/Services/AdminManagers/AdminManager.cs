using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using CourseWork.DataLayer.Repositories.Implementations;

namespace CourseWork.BusinessLogicLayer.Services.AdminManagers
{
    public class AdminManager : IAdminManager
    {
        private readonly Repository<UserInfo> _userInfoRepository;
        private readonly IMapper<UserListItemViewModel, UserInfo> _mapperList;
        private readonly IMapper<UserConfirmationViewModel, UserInfo> _mapperInfo;

        public AdminManager(IMapper<UserListItemViewModel, UserInfo> mapperList, Repository<UserInfo> userInfoRepository, ApplicationDbContext context, Repository<ApplicationUser> applicationUserRepository, IMapper<UserConfirmationViewModel, UserInfo> mapperInfo)
        {
            _mapperList = mapperList;
            _userInfoRepository = userInfoRepository;
            _mapperInfo = mapperInfo;
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

        public bool RespondToConfirmation(string userName, bool accept)
        {
            var user = _userInfoRepository.Get(userName);
            user.Status = accept ? UserStatus.Confirmed : UserStatus.WithoutConfirmation;
            return _userInfoRepository.UpdateRange(user);
        }
    }
}
