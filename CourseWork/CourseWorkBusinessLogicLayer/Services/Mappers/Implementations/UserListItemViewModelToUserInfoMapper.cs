using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
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
                LastLoginTime = item.LastLoginTime.ToString(),
                RegistrationTime = item.RegistrationTime.ToString(),
                ProjectNumber = item.ProjectNumber.ToString(),
                Raiting = item.Raiting.ToString(),
                Status = item.Status.ToString()
            };
        }
    }
}
