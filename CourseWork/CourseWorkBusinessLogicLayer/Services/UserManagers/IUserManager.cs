using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.ViewModels.CurrentUserViewModels;

namespace CourseWork.BusinessLogicLayer.Services.UserManagers
{
    public interface IUserManager
    {
        IIdentity CurrentUserIdentity { get; }

        string CurrentUserName { get; }

        Task<CurrentUserViewModel> GetCurrentUserInfo();

        IEnumerable<string> GetEmails(IEnumerable<string> userNames);
    }
}
