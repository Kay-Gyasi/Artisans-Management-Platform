namespace AMP.Processors.Processors
{
    [Processor]
    public class UserProcessor : ProcessorBase
    {
        private const string LookupCacheKey = "Userlookup";

        private readonly IAuthService _authService;
        private readonly ISmsMessaging _smsMessaging;

        public UserProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache,
            IAuthService authService,
            ISmsMessaging smsMessaging) : base(uow, mapper, cache)
        {
            _authService = authService;
            _smsMessaging = smsMessaging;
        }

        public async Task<Option<SigninResponse>> Login(SigninCommand command)
        {
            var user = await Uow.Users.Authenticate(command);
            return user is null ? null : new SigninResponse { Token = _authService.GenerateToken(user) };
        }

        public async Task<Result<bool>> SendPasswordResetLink(string phone)
        {
            var user = await Uow.Users.GetByPhone(phone);
            if (user is null)
                return new Result<bool>(new InvalidIdException($"User with phone: {phone} does not exist."));

            var confirmCode = Encoding.UTF8.GetString(user.PasswordKey)
                .RemoveSpecialCharacters();
            var message = MessageGenerator.SendPasswordResetLink(phone, 
                confirmCode, user.DisplayName);
            await _smsMessaging.Send(new SmsCommand {Message = message.Item1, Recipients = new[] {message.Item2}});
            return new Result<bool>(true);
        }

        public async Task<Result<bool>> ResetPassword(ResetPasswordCommand command)
        {
            var user = await Uow.Users.GetByPhoneAndConfirmCode(command.Phone, command.ConfirmCode);
            return user is null ? 
                new Result<bool>(new InvalidIdException("Invalid phone/password")) 
                : new Result<bool>(await RegisterUser(user, command));
        }

        public async Task<Result<string>> Update(UserCommand command)
        {
            var user = await Uow.Users.GetAsync(command.Id);
            if (user is null) return new Result<string>(
                new InvalidIdException($"User with id: {command.Id} does not exist."));
            await AssignFields(user, command);
            Cache.Remove(LookupCacheKey);
            await Uow.Users.UpdateAsync(user);
            await Uow.SaveChangesAsync();
            return new Result<string>(user.Id);
        }

        public async Task<PaginatedList<UserPageDto>> GetPage(PaginatedCommand command)
        {
            var page = await Uow.Users.GetPage(command, new CancellationToken());
            return Mapper.Map<PaginatedList<UserPageDto>>(page);
        }

        public async Task<Result<UserDto>> Get(string id)
        {
            var user = Mapper.Map<UserDto>(await Uow.Users.GetAsync(id));
            if (user is null) return new Result<UserDto>(
                new InvalidIdException($"User with id: {id} does not exist."));
            return new Result<UserDto>(user);
        }

        public async Task<Result<bool>> Delete(string id)
        {
            var user = await Uow.Users.GetAsync(id);
            if (user is null) return new Result<bool>(
                new InvalidIdException($"User with id: {id} does not exist."));
            Cache.Remove(LookupCacheKey);
            await Uow.Users.SoftDeleteAsync(user);
            await Uow.SaveChangesAsync();
            return new Result<bool>(true);
        }

        private async Task<bool> RegisterUser(Users user, ResetPasswordCommand command)
        {
            var passes = Uow.Users.Register(command.NewPassword);
            user.HasPassword(passes.Item1)
                .HasPasswordKey(passes.Item2);
            await Uow.Users.UpdateAsync(user);
            await Uow.SaveChangesAsync();
            return true;
        }

        private async Task AssignFields(Users user, UserCommand command)
        {
            command.Languages ??= new List<LanguagesCommand>();
            command.Address ??= new AddressCommand();
            command.Contact ??= new ContactCommand();
            var list = command.Languages.Select(lang => lang.Name).ToList();
            var languages = await Uow.Languages.BuildLanguages(list);
            user.WithFirstName(command.FirstName ?? "")
                .WithFamilyName(command.FamilyName ?? "")
                .WithOtherName(command.OtherName ?? "")
                .SetDisplayName()
                .WithImageId(command.ImageId ?? "")
                .HasLevelOfEducation(command.LevelOfEducation)
                .WithContact(Contact.Create(command.Contact.PrimaryContact ?? "")
                    .WithPrimaryContact2(command.Contact.PrimaryContact2 ?? "")
                    .WithPrimaryContact3(command.Contact.PrimaryContact3 ?? "")
                    .WithEmailAddress(command.Contact.EmailAddress ?? ""))
                .WithAddress(Address.Create(command.Address.City ?? "", command.Address.StreetAddress ?? "")
                    .WithStreetAddress2(command.Address.StreetAddress2 ?? "")
                    .FromTown(command.Address.Town ?? "")
                    .FromCountry(command.Address.Country))
                .Speaks(languages)
                .WithMomoNumber(command.MomoNumber ?? "");
        }
    }
}