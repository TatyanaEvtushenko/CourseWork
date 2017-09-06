using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Models;
using Microsoft.AspNetCore.Http;

namespace CourseWork.BusinessLogicLayer.Services.AccountConfirmationManagers
{
    public interface IAccountConfirmationManager
    {
        bool ConfirmAccount(string id, UserConfirmationViewModel model);
    }
}
