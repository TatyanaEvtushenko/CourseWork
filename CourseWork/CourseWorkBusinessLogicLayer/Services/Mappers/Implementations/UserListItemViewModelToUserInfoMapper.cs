using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using CourseWork.DataLayer.Repositories.Implementations;
using Microsoft.AspNetCore.Identity;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class UserListItemViewModelToUserInfoMapper : IMapper<UserListItemViewModel, UserInfo>
    {
        private readonly Dictionary<string, string> _usernames;

        public UserListItemViewModelToUserInfoMapper(Repository<ApplicationUser> repository)
        {
            _usernames = ((ApplicationUserRepository) repository).GetIdUsernameDictionary();
        }

        public UserInfo ConvertTo(UserListItemViewModel item)
        {
            throw new System.NotImplementedException();
        }

        public UserListItemViewModel ConvertFrom(UserInfo item)
        {
            return new UserListItemViewModel
            {
                Username = _usernames[item.UserId],
                LastLoginTime = item.LastLoginTime.ToString(),
                RegistrationTime = item.RegistrationTime.ToString(),
                ProjectNumber = item.ProjectNumber.ToString(),
                Raiting = item.Raiting.ToString(),
                Status = item.Status.ToString()
            };
        }
    }
}
