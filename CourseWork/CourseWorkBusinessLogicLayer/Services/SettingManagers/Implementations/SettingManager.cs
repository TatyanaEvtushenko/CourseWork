using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.ViewModels.SettingViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Enums.Configurations;

namespace CourseWork.BusinessLogicLayer.Services.SettingManagers.Implementations
{
    public class SettingManager : ISettingManager
    {
        private readonly IMapper<RoleNamesViewModel, Dictionary<UserRole, string>> _roleMapper;

        public SettingManager(IMapper<RoleNamesViewModel, Dictionary<UserRole, string>> roleMapper)
        {
            _roleMapper = roleMapper;
        }

        public RoleNamesViewModel GetRoles()
        {
            return _roleMapper.ConvertFrom(EnumConfiguration.RoleNames);
        }
    }
}
