using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Enums.Configurations;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class UserListItemViewModelToUserInfoMapper : IMapper<UserListItemViewModel, UserInfo>
    {
        public UserInfo ConvertTo(UserListItemViewModel item)
        {
            throw new System.NotImplementedException();
        }

        public UserListItemViewModel ConvertFrom(UserInfo item)
        {
            return new UserListItemViewModel
            {
                Username = item.UserName,
                LastLoginTime = item.LastLoginTime.ToString(),
                RegistrationTime = item.RegistrationTime.ToString(),
                ProjectNumber = 0.ToString(), // WILL BE CORRECTED IN NEXT MERGE!!!
                Raiting = item.Rating.ToString(),
                Status = EnumConfiguration.StatusDisplayNames[item.Status],
                StatusCode = (int)item.Status,
                IsBlocked = item.IsBlocked
            };
        }
    }
}
