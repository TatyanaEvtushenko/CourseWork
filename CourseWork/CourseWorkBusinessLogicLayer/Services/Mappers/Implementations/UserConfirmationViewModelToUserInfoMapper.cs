using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Enums.Configurations;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class UserConfirmationViewModelToUserInfoMapper : IMapper<UserConfirmationViewModel, UserInfo>
    {
        public UserInfo ConvertTo(UserConfirmationViewModel item)
        {
            throw new System.NotImplementedException();
        }

        public UserConfirmationViewModel ConvertFrom(UserInfo item)
        {
            return new UserConfirmationViewModel
            {
                PassportScan = item.PassportScan,
                Name = item.Name,
                Surname = item.Surname,
                Description = item.Description
            };
        }
    }
}
