using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace AMP.Processors.Repositories
{
    public interface ICloudStorageService
    {
        Task<ImageUploadResult> UploadPhotoAsync(IFormFile photo);
    }
}