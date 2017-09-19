using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;

namespace CourseWork.BusinessLogicLayer.Services.AdminManagers
{
    public interface IAdminManager
    {
        UserListItemViewModel[] GetAllUsers();

        UserListItemViewModel[] GetFilteredUsers(FilterRequestViewModel model);

        UserConfirmationViewModel GetPersonalInfo(string userName);

        Task<bool> RespondToConfirmation(string userName, bool accept);

        UserListItemViewModel[] SortByField(string fieldName, bool ascending);

        bool BlockUnblock(string[] usersToBlock);

        bool Delete(string[] usersToDelete, bool withCommentsAndRaitings);
    }
}
