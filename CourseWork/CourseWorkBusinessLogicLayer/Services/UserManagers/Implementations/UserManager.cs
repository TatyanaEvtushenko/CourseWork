using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Services.Mappers;
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
        private readonly IPhotoManager _photoManager;
        private readonly IMapper<CurrentUserViewModel, UserInfo> _userMapper;
	    private readonly IRepository<UserInfo> _userInfoRepository;
        private readonly IRepository<ApplicationUser> _applicationUserRepository;

        public UserManager(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager,
            IRepository<UserInfo> userInfoRepository, IRepository<ApplicationUser> applicationUserRepository,
            IPhotoManager photoManager,
            IMapper<CurrentUserViewModel, UserInfo> userMapper)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _applicationUserRepository = applicationUserRepository;
            _photoManager = photoManager;
            _userMapper = userMapper;
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
            var userInfo = _userInfoRepository.FirstOrDefault(i => i.UserName == CurrentUserName, 
                i => i.ApplicationUser);
            var viewModel = _userMapper.ConvertFrom(userInfo);
            viewModel.Role = (await _userManager.GetRolesAsync(userInfo.ApplicationUser)).ElementAt(0);
            return viewModel;
        }
    }
}
