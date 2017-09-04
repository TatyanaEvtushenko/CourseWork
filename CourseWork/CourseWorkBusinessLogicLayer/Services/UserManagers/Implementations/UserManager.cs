using System.Linq;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.ViewModels.CurrentUserViewModels;
using CourseWork.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CourseWork.BusinessLogicLayer.Services.UserManagers.Implementations
{
    public class UserManager : IUserManager
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManager(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task<CurrentUserViewModel> GetCurrentUserInfo()
        {
            var user = _contextAccessor.HttpContext.User.Identity;
            var applicationUser = await _userManager.FindByNameAsync(user.Name);
            return !user.IsAuthenticated ? null : new CurrentUserViewModel
            { 
                UserName = user.Name,
                Role = (await _userManager.GetRolesAsync(applicationUser)).ElementAt(0)
            };
        }
    }
}
