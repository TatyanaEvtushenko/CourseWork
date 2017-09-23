using System;
using System.IO;
using System.Text.RegularExpressions;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CourseWork.BusinessLogicLayer.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace CourseWork.BusinessLogicLayer.Services.PhotoManagers.Implementations
{
    public class PhotoManager : IPhotoManager
    {
        private readonly Cloudinary _cloudinary;

        private readonly IHostingEnvironment _hostingEnvironment;

        public PhotoManager(IOptions<CloudinaryOptions> options, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _cloudinary = InitializeCloudinary(options);
        }

        public string LoadImage(string imageEncoded)
        {
            if (IsUrl(imageEncoded))
            {
                return imageEncoded;
            }
            var savedImagePath = SaveFile(imageEncoded);
            var imageUrl = Upload(savedImagePath);
            return imageUrl;
        }

        private Cloudinary InitializeCloudinary(IOptions<CloudinaryOptions> options)
        {
            var optionsValue = options.Value;
            var cloudinaryAccount = new Account(optionsValue.CloudName, optionsValue.ApiKey, optionsValue.ApiSecret);
            return new Cloudinary(cloudinaryAccount);
        }

        private bool IsUrl(string image)
        {
            return Uri.IsWellFormedUriString(image, UriKind.Absolute);
        }

        private string SaveFile(string imageEncoded)
        {
            var imageDecoded = GetDecodedImage(imageEncoded);
            var filePath = GetImageRandomPath();
            File.WriteAllBytes(filePath, imageDecoded);
            return filePath;
        }

        private byte[] GetDecodedImage(string imageEncoded)
        {
            const string regexExpression = @"data:(.*?);base64,";
            var imageForDecoding = Regex.Replace(imageEncoded, regexExpression, String.Empty);
            return Convert.FromBase64String(imageForDecoding);
        }

        private string GetImageRandomPath() => 
            Path.Combine(_hostingEnvironment.WebRootPath, Guid.NewGuid().ToString("N") + ".jpg");

        private string Upload(string imagePath)
        {
            var fileInfo = new FileInfo(imagePath);
            if (!fileInfo.Exists)
            {
                return null;
            }
            var uploadResult = UploadToCloudinary(imagePath);
            fileInfo.Delete();
            return uploadResult.Uri.AbsoluteUri;
        }

        private ImageUploadResult UploadToCloudinary(string imagePath)
        {
            var uploadParams = new ImageUploadParams { File = new FileDescription(imagePath) };
            return _cloudinary.UploadAsync(uploadParams).Result;
        }
    }
}
