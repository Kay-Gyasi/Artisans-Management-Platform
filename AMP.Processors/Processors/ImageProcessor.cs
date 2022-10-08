using System;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Commands;
using AMP.Processors.Interfaces.UoW;
using AMP.Processors.Processors.Base;
using AMP.Processors.Repositories;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace AMP.Processors.Processors
{
    [Processor]
    public class ImageProcessor : ProcessorBase
    {
        private readonly ICloudStorageService _cloudinary;

        public ImageProcessor(IUnitOfWork uow, 
            IMapper mapper, 
            IMemoryCache cache,
            ICloudStorageService cloudinary) : base(uow, mapper, cache)
        {
            _cloudinary = cloudinary;
        }

        public async Task<bool> UploadImage(ImageCommand command)
        {
            try
            {
                if (command.Image is null) return false;
                var result = await _cloudinary.UploadPhotoAsync(command.Image);
                if (result.Error != null) return false;

                await Uow.Images.RemoveCurrentDetails(command.UserId);
                var image = Images.Create(result.SecureUrl.AbsoluteUri, result.PublicId)
                    .ForUserWithId(command.UserId);
                await Uow.Images.InsertAsync(image);
                var user = await Uow.Users.GetAsync(command.UserId);
                user.WithImageId(image.Id);
                await Uow.Users.UpdateAsync(user);
                await Uow.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}