using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Services.PhotoManagers;
using CourseWork.BusinessLogicLayer.ViewModels.AccountViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.CurrentUserViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CourseWork.BusinessLogicLayer.Services.UserManagers.Implementations
{
    public class UserManager : IUserManager
    {
        public IIdentity CurrentUserIdentity => _contextAccessor.HttpContext.User.Identity;

        public string CurrentUserName => _contextAccessor.HttpContext.User.Identity.Name;

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
	    private readonly Repository<UserInfo> _userInfoRepository;
        private readonly Repository<ApplicationUser> _applicationUserRepository;
        private readonly IPhotoManager _photoManager;

        public UserManager(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager,
            Repository<UserInfo> userInfoRepository,
            Repository<ApplicationUser> applicationUserRepository, IPhotoManager photoManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _applicationUserRepository = applicationUserRepository;
            _photoManager = photoManager;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<CurrentUserViewModel> GetCurrentUserInfo()
        {
            if (!CurrentUserIdentity.IsAuthenticated)
            {
                return null;
            }
            return await GetCurrentUser();
        }

        public IEnumerable<string> GetEmails(IEnumerable<string> userNames)
        {
            return _applicationUserRepository.GetWhere(user => userNames.Contains(user.UserName)).Select(user => user.Email);
        }

        public void Edit(AccountEditViewModel newInfo)
        {
            var currentUser = _userInfoRepository.FirstOrDefault(item => item.UserName == CurrentUserName);
            currentUser.About = newInfo.About;
            currentUser.Contacts = newInfo.Contacts;
            _userInfoRepository.UpdateRange(currentUser);
        }

        public string ChangeAvatar(string newAvatarB64)
        {
            var currentUser = _userInfoRepository.FirstOrDefault(item => item.UserName == CurrentUserName);
            var newAvatar = _photoManager.LoadImage(newAvatarB64);
            currentUser.Avatar = newAvatar;
            _userInfoRepository.UpdateRange(currentUser);
            return newAvatar;
        }

        private async Task<CurrentUserViewModel> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(CurrentUserName);
            return new CurrentUserViewModel
            {
                UserName = CurrentUserName,
                Role = (await _userManager.GetRolesAsync(user)).ElementAt(0),
                IsBlocked = _userInfoRepository.FirstOrDefault(u => u.UserName == CurrentUserName).IsBlocked
            };
        }
    }
}
