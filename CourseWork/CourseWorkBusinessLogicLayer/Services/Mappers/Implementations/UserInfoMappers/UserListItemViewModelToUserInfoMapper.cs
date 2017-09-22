using System.Linq;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Enums.Configurations;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.UserInfoMappers
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
                ProjectNumber = item.Projects.Count().ToString(),
                Raiting = "0",//item.Rating.ToString(),
                Status = EnumConfiguration.StatusDisplayNames[item.Status],
                StatusCode = (int) item.Status,
                IsBlocked = item.IsBlocked
            };
        }
    }
}