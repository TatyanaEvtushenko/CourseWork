using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using CourseWork.BusinessLogicLayer.Services.PhotoManagers;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

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

        public bool ConfirmAccount(string id, UserConfirmationViewModel model)
        {
            var user = _userRepository.Get(id);
            if (user.Status == UserStatus.WithoutConfirmation && model != null)
            {
                return RequestConfirmation(user, model);
            }
            return false;
        }

        private bool RequestConfirmation(UserInfo user, UserConfirmationViewModel model)
        {
            user.PassportScan = model.PassportScan;
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Description = model.Description;
            user.Status = UserStatus.AwaitingConfirmation;
            return _userRepository.UpdateRange(user);
        }

        private string UploadPassportScan(IFormFile imageFile)
        {
            var savedImagePath = SaveFile(imageFile);
            var imageUrl = _photoManager.Upload(savedImagePath);
            return imageUrl;
        }

        private string SaveFile(IFormFile imageFile)
        {
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, Guid.NewGuid().ToString("N")
                + Path.GetExtension(imageFile.FileName));
            imageFile.CopyTo(new FileStream(filePath, FileMode.Create));
            return filePath;
        }
    }
}
