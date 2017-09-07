using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;

namespace CourseWork.BusinessLogicLayer.Services.AdminManagers
{
    public interface IAdminManager
    {
        UserListItemViewModel[] GetAllUsers();
    }
}
