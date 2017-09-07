using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using System;
using System.IO;
using System.Text.RegularExpressions;
using CourseWork.BusinessLogicLayer.Services.PhotoManagers;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Microsoft.AspNetCore.Hosting;

namespace CourseWork.BusinessLogicLayer.Services.AccountConfirmationManagers.Implementations
{
    public class AccountConfirmationManager : IAccountConfirmationManager
    {
        private readonly Repository<UserInfo> _userRepository;
        private readonly IPhotoManager _photoManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AccountConfirmationManager(Repository<UserInfo> userRepository, IPhotoManager photoManager, IHostingEnvironment hostingEnvironment)
        {
            _userRepository = userRepository;
            _photoManager = photoManager;
            _hostingEnvironment = hostingEnvironment;
        }

        public bool ConfirmAccount(string userName, UserConfirmationViewModel model)
        {
            var user = _userRepository.Get(userName);
            if (user.Status == UserStatus.WithoutConfirmation && model != null)
            {
                return RequestConfirmation(user, model);
            }
            return false;
        }

        private bool RequestConfirmation(UserInfo user, UserConfirmationViewModel model)
        {
            user.PassportScan = UploadPassportScan(model.PassportScan);
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Description = model.Description;
            user.Status = UserStatus.AwaitingConfirmation;
            return _userRepository.UpdateRange(user);
        }

        private string UploadPassportScan(string imageEncoded)
        {
            var savedImagePath = SaveFile(imageEncoded);
            var imageUrl = _photoManager.Upload(savedImagePath);
            return imageUrl;
        }

        private string SaveFile(string imageEncoded)
        {
            byte[] imageDecoded = Convert.FromBase64String(Regex.Replace(imageEncoded, @"data:(.*?);base64,", String.Empty));
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, Guid.NewGuid().ToString("N") + ".jpg");
            File.WriteAllBytes(filePath, imageDecoded);
            return filePath;
        }
    }
}
