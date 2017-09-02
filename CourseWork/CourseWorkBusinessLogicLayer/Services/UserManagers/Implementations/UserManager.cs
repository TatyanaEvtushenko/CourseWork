using System.Security.Claims;
using CourseWork.BusinessLogicLayer.ViewModels.CurrentUserViewModels;

namespace CourseWork.BusinessLogicLayer.Services.UserManagers.Implementations
{
    public class UserManager : IUserManager
    {
        public CurrentUserViewModel GetCurrentUserInfo()
        {
            return new CurrentUserViewModel
            {
                IsAuthenticated = ClaimsPrincipal.Current != null && ClaimsPrincipal.Current.Identity.IsAuthenticated,
                UserName = ClaimsPrincipal.Current == null ? null : ClaimsPrincipal.Current.Identity.Name,
            };
        }
    }
}
