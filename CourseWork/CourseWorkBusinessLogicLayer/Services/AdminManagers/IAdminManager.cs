using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;

namespace CourseWork.BusinessLogicLayer.Services.AdminManagers
{
    public interface IAdminManager
    {
        UserListItemViewModel[] GetAllUsers();

        UserListItemViewModel[] GetFilteredUsers(bool confirmed, bool requested, bool unconfirmed);

        UserConfirmationViewModel GetPersonalInfo(string userName);

        bool RespondToConfirmation(string userName, bool accept);
    }
}
