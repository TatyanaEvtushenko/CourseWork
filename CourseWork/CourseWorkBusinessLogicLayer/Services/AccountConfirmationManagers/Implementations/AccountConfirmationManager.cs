using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CourseWork.BusinessLogicLayer.Services.AccountConfirmationManagers.Implementations
{
    public class AccountConfirmationManager : IAccountConfirmationManager
    {
        private readonly IRepository<UserInfo> _userRepository;

        public AccountConfirmationManager(IRepository<UserInfo> userRepository)
        {
            _userRepository = userRepository;
        }

        public bool ConfirmAccount(string id, UserConfirmationViewModel model)
        {
            var user = _userRepository.Get(id);
            if (user.Status == UserStatus.WithoutConfirmation && model != null)
            {
                return RequestConfirmation(user, model);
            }
            return false;
        }

        private bool RequestConfirmation(UserInfo user, UserConfirmationViewModel model)
        {
            user.PassportScan = model.PassportScan;
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Description = model.Description;
            user.Status = UserStatus.AwaitingConfirmation;
            return _userRepository.UpdateRange(user);
        }
    }
}
