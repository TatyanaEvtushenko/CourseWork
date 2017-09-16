using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.ViewModels.CurrentUserViewModels;

namespace CourseWork.BusinessLogicLayer.Services.UserManagers
{
    public interface IUserManager
    {
        Task<CurrentUserViewModel> GetCurrentUserInfo();

        IEnumerable<string> GetEmails(IEnumerable<string> userNames);

        void Edit(string newAbout);

        void ChangeAvatar(string newAvatarB64);
    }
}
