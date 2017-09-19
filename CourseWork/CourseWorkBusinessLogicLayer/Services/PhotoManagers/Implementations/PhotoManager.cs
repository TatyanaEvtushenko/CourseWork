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
            var optionsValue = options.Value;
            _cloudinary = new Cloudinary(new Account(optionsValue.CloudName, optionsValue.ApiKey, optionsValue.ApiSecret));
        }

        public string LoadImage(string imageEncoded)
        {
            if (Uri.IsWellFormedUriString(imageEncoded, UriKind.Absolute))
            {
                return imageEncoded;
            }
            var savedImagePath = SaveFile(imageEncoded);
            var imageUrl = Upload(savedImagePath);
            return imageUrl;
        }

        private string SaveFile(string imageEncoded)
        {
            byte[] imageDecoded = Convert.FromBase64String(Regex.Replace(imageEncoded, @"data:(.*?);base64,", String.Empty));
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, Guid.NewGuid().ToString("N") + ".jpg");
            File.WriteAllBytes(filePath, imageDecoded);
            return filePath;
        }

        private string Upload(string imagePath)
        {
            var fileInfo = new FileInfo(imagePath);
            if (!fileInfo.Exists) return null;
            var uploadParams = new ImageUploadParams { File = new FileDescription(imagePath) };
            var uploadResult = _cloudinary.UploadAsync(uploadParams).Result;
            fileInfo.Delete();
            return uploadResult.Uri.AbsoluteUri;
        }
    }
}
