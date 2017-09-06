using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.ViewModels.CurrentUserViewModels;

namespace CourseWork.BusinessLogicLayer.Services.UserManagers
{
    public interface IUserManager
    {
        Task<CurrentUserViewModel> GetCurrentUserInfo();
    }
}
