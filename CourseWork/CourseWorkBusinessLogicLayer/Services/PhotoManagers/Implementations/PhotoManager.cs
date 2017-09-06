using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CourseWork.BusinessLogicLayer.Options;
using Microsoft.Extensions.Options;

namespace CourseWork.BusinessLogicLayer.Services.PhotoManagers.Implementations
{
    public class PhotoManager : IPhotoManager
    {
        private readonly Cloudinary _cloudinary;

        public PhotoManager(IOptions<CloudinaryOptions> options)
        {
            var optionsValue = options.Value;
            _cloudinary = new Cloudinary(new Account(optionsValue.CloudName, optionsValue.ApiKey, optionsValue.ApiSecret));
        }

        public string Upload(string imagePath)
        {
            var fileInfo = new FileInfo(imagePath);
            if (!fileInfo.Exists) return null;
            var uploadParams = new ImageUploadParams {File = new FileDescription(imagePath)};
            var uploadResult = _cloudinary.UploadAsync(uploadParams).Result;
            fileInfo.Delete();
            return uploadResult.Uri.AbsoluteUri;
        }
    }
}
