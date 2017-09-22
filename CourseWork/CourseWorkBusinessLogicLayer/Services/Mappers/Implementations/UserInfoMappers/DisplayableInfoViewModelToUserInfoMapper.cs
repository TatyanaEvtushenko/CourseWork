using System;
using System.Linq;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.UserInfoMappers
{
    public class DisplayableInfoViewModelToUserInfoMapper : IMapper<DisplayableInfoViewModel, UserInfo>
    {
        public UserInfo ConvertTo(DisplayableInfoViewModel item)
        {
            throw new NotImplementedException();
        }

        public DisplayableInfoViewModel ConvertFrom(UserInfo item)
        {
            return new DisplayableInfoViewModel
            {
                UserName = item.UserName,
                RegistrationTime = item.RegistrationTime.ToString(),
                Avatar = item.Avatar,
                About = item.About,
                ProjectNumber = item.Projects.Count(),
                Contacts = item.Contacts
            };
        }
    }
}
