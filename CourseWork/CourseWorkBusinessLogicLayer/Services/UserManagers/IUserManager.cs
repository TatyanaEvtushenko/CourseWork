using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.ViewModels.AccountViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.CurrentUserViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.UserManagers
{
    public interface IUserManager
    {
        IIdentity CurrentUserIdentity { get; }

        string CurrentUserName { get; }

        Task<CurrentUserViewModel> GetCurrentUserInfo();

        IEnumerable<string> GetEmails(IEnumerable<string> userNames);

        UserInfo GetCurrentUserUserInfo();

        void Edit(AccountEditViewModel newAbout);

        void ChangeAvatar(string newAvatarB64);
    }
}
