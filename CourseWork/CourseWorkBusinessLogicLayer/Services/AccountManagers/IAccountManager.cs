using System.Threading.Tasks;

namespace CourseWork.BusinessLogicLayer.Services.AccountManagers
{
    public interface IAccountManager
    {
        Task<bool> Register(string userName, string email, string password, string confirmPassword);
    }
}
