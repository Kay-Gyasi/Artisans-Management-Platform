using System.Threading.Tasks;
using AMP.Processors.Repositories;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AMP.Services.Images
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IOptions<CloudinaryOptions> _options;
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinaryOptions> options)
        {
            _options = options;
            var account = new Account(
                _options.Value.CloudName,
                _options.Value.ApiKey,
                _options.Value.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }
        public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile photo)
        {
            var uploadResult = new ImageUploadResult();
            if (photo.Length <= 0) return uploadResult;
            await using var stream = photo.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(photo.FileName, stream),
                Transformation = new Transformation()
                    .Height(500) //check
                    .Width(800)
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult;
        }
    }
}