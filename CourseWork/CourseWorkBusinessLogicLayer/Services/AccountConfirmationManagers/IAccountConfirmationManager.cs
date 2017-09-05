using System;
using System.Collections.Generic;
using System.Text;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CourseWork.BusinessLogicLayer.Services.AccountConfirmationManagers
{
    public interface IAccountConfirmationManager
    {
        bool ConfirmAccount(string id, UserConfirmationViewModel model);
    }
}
