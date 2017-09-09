using System.Linq;
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
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
	    private readonly Repository<UserInfo> _userInfoRepository;

        public UserManager(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager, Repository<UserInfo> userInfoRepository)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
	        _userInfoRepository = userInfoRepository;
        }

        public async Task<CurrentUserViewModel> GetCurrentUserInfo()
        {
            var user = _contextAccessor.HttpContext.User.Identity;
            return !user.IsAuthenticated ? null : new CurrentUserViewModel
            { 
                UserName = user.Name,
                Role = (await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(user.Name))).ElementAt(0),
				IsBlocked = _userInfoRepository.Get(user.Name).IsBlocked
            };
        }
    }
}
