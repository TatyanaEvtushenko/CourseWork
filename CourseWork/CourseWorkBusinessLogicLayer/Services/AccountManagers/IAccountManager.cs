using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.AccountManagers
{
    public interface IAccountManager
    {
        Task<bool> Register(string userName, string email, string password, string messageSubsject, string messagePrototype);

        Task<bool> ConfirmRegistration(string userId, string code);

        Task<bool> Login(string email, string password);

        Task Logout();

        Task AddRole(string userName, UserRole role);

        Task RemoveRole(string userName, UserRole role);

        DisplayableInfoViewModel[] GetDisplayableInfo(string[] userNames);

        DisplayableInfoViewModel GetUserDisplayableInfo(string username);
    }
}
