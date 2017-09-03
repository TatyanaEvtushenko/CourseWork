using System.Threading.Tasks;

namespace CourseWork.BusinessLogicLayer.Services.AccountManagers
{
    public interface IAccountManager
    {
        Task<bool> Register(string userName, string email, string password);

        Task<bool> ConfirmRegistration(string userId, string code);

        Task<bool> Login(string email, string password);

        Task Logout();
    }
}
