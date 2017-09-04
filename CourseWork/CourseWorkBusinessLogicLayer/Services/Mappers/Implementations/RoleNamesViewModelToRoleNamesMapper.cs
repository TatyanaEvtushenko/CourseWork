using System;
using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.SettingViewModels;
using CourseWork.DataLayer.Enums;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class RoleNamesViewModelToRoleNamesMapper : IMapper<RoleNamesViewModel, Dictionary<UserRole, string>>
    {
        public Dictionary<UserRole, string> ConvertTo(RoleNamesViewModel item)
        {
            throw new NotImplementedException();
        }

        public RoleNamesViewModel ConvertFrom(Dictionary<UserRole, string> item)
        {
            return new RoleNamesViewModel
            {
                Admin = item[UserRole.Admin],
                User = item[UserRole.User],
                ConfirmedUser = item[UserRole.ConfirmedUser]
            };
        }
    }
}
