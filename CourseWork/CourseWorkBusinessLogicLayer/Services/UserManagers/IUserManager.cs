using CourseWork.BusinessLogicLayer.ViewModels.CurrentUserViewModels;

namespace CourseWork.BusinessLogicLayer.Services.UserManagers
{
    public interface IUserManager
    {
        CurrentUserViewModel GetCurrentUserInfo();
    }
}
