using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
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
