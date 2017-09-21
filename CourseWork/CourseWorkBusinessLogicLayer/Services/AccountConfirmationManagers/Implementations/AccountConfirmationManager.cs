using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.BusinessLogicLayer.Services.PhotoManagers;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using CourseWork.DataLayer.Repositories.Implementations;

namespace CourseWork.BusinessLogicLayer.Services.AccountConfirmationManagers.Implementations
{
    public class AccountConfirmationManager : IAccountConfirmationManager
    {
        private readonly Repository<UserInfo> _userRepository;
        private readonly IPhotoManager _photoManager;

        public AccountConfirmationManager(Repository<UserInfo> userRepository, IPhotoManager photoManager)
        {
            _userRepository = userRepository;
            _photoManager = photoManager;
        }

        public bool ConfirmAccount(string userName, UserConfirmationViewModel model)
        {
            var user = _userRepository.FirstOrDefault(p => p.UserName == userName);
            if (user.Status == UserStatus.WithoutConfirmation && model != null)
            {
                return RequestConfirmation(user, model);
            }
            return false;
        }

        private bool RequestConfirmation(UserInfo user, UserConfirmationViewModel model)
        {
            user.PassportScan = _photoManager.LoadImage(model.PassportScan);
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Description = model.Description;
            user.Status = UserStatus.AwaitingConfirmation;
            return _userRepository.UpdateRange(user);
        }
    }
}
