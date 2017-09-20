using System;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.UserInfoMappers
{
    public class UserSmallViewModelToUserInfoMapper : IMapper<UserSmallViewModel, UserInfo>
    {
        public UserInfo ConvertTo(UserSmallViewModel item)
        {
            throw new NotImplementedException();
        }

        public UserSmallViewModel ConvertFrom(UserInfo item)
        {
            return new UserSmallViewModel
            {
                UserName = item.UserName
            };
        }
    }
}
