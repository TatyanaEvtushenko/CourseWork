using CourseWork.BusinessLogicLayer.ViewModels.CurrentUserViewModels;
using Microsoft.AspNetCore.Http;

namespace CourseWork.BusinessLogicLayer.Services.UserManagers.Implementations
{
    public class UserManager : IUserManager
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserManager(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public CurrentUserViewModel GetCurrentUserInfo()
        {
            var user = _contextAccessor.HttpContext.User.Identity;
            return !user.IsAuthenticated ? null : new CurrentUserViewModel
            { 
                UserName = user.Name,
            };
        }
    }
}
