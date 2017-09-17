using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
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

        public UserManager(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager,
            Repository<UserInfo> userInfoRepository,
            Repository<ApplicationUser> applicationUserRepository)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _applicationUserRepository = applicationUserRepository;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<CurrentUserViewModel> GetCurrentUserInfo()
        {
            var user = CurrentUserIdentity;
            return !user.IsAuthenticated ? null : new CurrentUserViewModel
            { 
                UserName = user.Name,
                Role = (await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(user.Name))).ElementAt(0),
				IsBlocked = _userInfoRepository.Get(user.Name).IsBlocked
            };
        }

        public UserInfo GetCurrentUserUserInfo()
        {
            return _userInfoRepository.Get(CurrentUserName);
        }

        public IEnumerable<string> GetEmails(IEnumerable<string> userNames)
        {
            return _applicationUserRepository.GetWhere(user => userNames.Contains(user.UserName)).Select(user => user.Email);
        }
    }
}
