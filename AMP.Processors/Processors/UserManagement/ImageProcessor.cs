using AMP.Domain.Entities.UserManagement;
using AMP.Processors.Commands.UserManagement;
using AMP.Processors.QueryObjects;

namespace AMP.Processors.Processors.UserManagement
{
    [Processor]
    public class ImageProcessor : Processor
    {
        private readonly IAuthService _authService;
        private readonly ICloudStorageService _cloudinary;
        private User _userForToken;

        public ImageProcessor(IUnitOfWork uow, 
            IMapper mapper, 
            IMemoryCache cache,
            IAuthService authService,
            ICloudStorageService cloudinary) : base(uow, mapper, cache)
        {
            _authService = authService;
            _cloudinary = cloudinary;
        }

        public async Task<SigninResponse> UploadImage(ImageCommand command)
        {
            
            if (command.Image is null) return null;
            var result = await _cloudinary.UploadPhotoAsync(command.Image);
            if (result.Error != null) return null;

            var executionStrategy = Uow.GetExecutionStrategy();
            await executionStrategy.ExecuteAsync(async () =>
            {

                await using var transaction = Uow.BeginTransaction();
                try
                {
                    await Uow.Images.RemoveCurrentDetails(command.UserId);
                    var image = Image.Create(result.SecureUrl.AbsoluteUri, result.PublicId)
                        .ForUserWithId(command.UserId);
                    await Uow.Images.InsertAsync(image);
                    var user = await Uow.Users.GetAsync(command.UserId);
                    user.WithImageId(image.Id);
                    await Uow.Users.UpdateAsync(user);
                    await Uow.SaveChangesAsync();
                    await transaction.CommitAsync();
                    _userForToken = user;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
            return await GetToken();
        }

        private async Task<SigninResponse> GetToken()
        {
            return new SigninResponse
            {
                Token = await Task.FromResult(_authService.GenerateToken(new LoginQueryObject
                {
                    Password = _userForToken.Password,
                    PasswordKey = _userForToken.PasswordKey,
                    Contact_PrimaryContact = _userForToken.Contact.PrimaryContact,
                    Id = _userForToken.Id,
                    DisplayName = _userForToken.DisplayName,
                    FamilyName = _userForToken.FamilyName,
                    ImageUrl = _userForToken.Image.ImageUrl,
                    Contact_EmailAddress = _userForToken.Contact.EmailAddress,
                    Address_StreetAddress = _userForToken.Address.StreetAddress,
                    Type = _userForToken.Type.ToString()
                }))
            };
        }
    }
}