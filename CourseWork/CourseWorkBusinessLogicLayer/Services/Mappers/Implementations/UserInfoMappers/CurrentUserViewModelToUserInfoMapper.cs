using System;
using CourseWork.BusinessLogicLayer.ViewModels.CurrentUserViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.UserInfoMappers
{
    public class CurrentUserViewModelToUserInfoMapper : IMapper<CurrentUserViewModel, UserInfo>
    {
        public UserInfo ConvertTo(CurrentUserViewModel item)
        {
            throw new NotImplementedException();
        }

        public CurrentUserViewModel ConvertFrom(UserInfo item)
        {
            return new CurrentUserViewModel
            {
                UserName = item.UserName,
                IsBlocked = item.IsBlocked,
                Avatar = item.Avatar
            };
        }
    }
}
