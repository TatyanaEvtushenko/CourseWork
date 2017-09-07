using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;

namespace CourseWork.BusinessLogicLayer.Services.AdminManagers
{
    public interface IAdminManager
    {
        List<UserListItemViewModel> GetAllUsers();
    }
}
