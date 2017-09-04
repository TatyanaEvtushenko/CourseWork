using System;
using System.Collections.Generic;
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
        private readonly CloudinaryOptions _options;

        public PhotoManager(IOptions<CloudinaryOptions> options)
        {
            _options = options.Value;
            _cloudinary = new Cloudinary(new Account(_options.CloudName, _options.ApiKey, _options.ApiSecret));
        }

        public string Upload(string imagePath)
        {
            var uploadParams = new ImageUploadParams {File = new FileDescription(imagePath)};
            var uploadResult = _cloudinary.UploadAsync(uploadParams).Result;
            return _options.UrlPrefix + uploadResult.PublicId + ".jpg";
        }
    }
}
