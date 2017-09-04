using System;
using System.Collections.Generic;
using System.Text;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.BusinessLogicLayer.Services.AccountConfirmationManagers
{
    public interface IAccountConfirmationManager
    {
        bool ConfirmAccount(string id, UserConfirmationViewModel model);
    }
}
