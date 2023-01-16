using System.Data.Common;
using AMP.Domain.Entities.UserManagement;
using AMP.Processors.Commands.UserManagement;

namespace AMP.Processors.Processors.UserManagement;

[Processor]
public class RegistrationProcessor : ProcessorBase
{
    private readonly ISmsMessaging _smsMessaging;
    private const string LookupCacheKey = "Userlookup";

    public RegistrationProcessor(IUnitOfWork uow, IMapper mapper, 
        IMemoryCache cache,
        ISmsMessaging smsMessaging) : base(uow, mapper, cache)
    {
        _smsMessaging = smsMessaging;
    }

    public async Task<Result<string>> Save(UserCommand command)
    {
        var userExists = await Uow.Users.Exists(command.Contact.PrimaryContact);
        if (userExists)
            return new Result<string>(
                new UserAlreadyExistsException($"User with contact {command.Contact.PrimaryContact} already exists."));

        // decide on isolation level
        var userId = "";
        var executionStrategy = Uow.GetExecutionStrategy();
        await executionStrategy.ExecuteAsync(async () =>
        {
            await using var transaction = Uow.BeginTransaction();
            try
            {
                var saveUserTask = SaveUser(command);
                var saveRegistrationTask = SaveRegistration(command.Contact.PrimaryContact);
                await Task.WhenAll(saveUserTask, saveRegistrationTask);
                var user = await saveUserTask;
                var code = await saveRegistrationTask;
                await Uow.SaveChangesAsync();
                await Task.WhenAll(SendVerificationLink(user.Contact.PrimaryContact, code),
                    PostAsType(user, transaction.GetDbTransaction(), Uow.GetDbConnection()));
                await transaction.CommitAsync();
                userId = user.Id;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        });
        return new Result<string>(userId);
    }

    public async Task<Result<bool>> VerifyUser(string phone, string code)
    {
        var isMatch = await Uow.Registrations.Crosscheck(phone, code);
        if (!isMatch) return new 
            Result<bool>(new UserVerificationFailedException("Invalid phone/verification code"));

        await Uow.Registrations.Verify(phone, code);
        Cache.Remove(LookupCacheKey);
        return new Result<bool>(true);
    }

    public async Task<Result<bool>> SendVerificationLink(string phone, string code)
    {
        if (string.IsNullOrEmpty(code))
        {
            code = RandomStringHelper.Generate(20, true);
            var registration = await Uow.Registrations.GetByPhone(phone);
            if (registration is null) return new Result<bool>(new 
                NullRegistrationException($"Registration for phone {phone} does not exist"));
            registration.HasVerificationCode(code);
            await Uow.SaveChangesAsync();
        }
        
        var message = MessageGenerator.SendVerificationLink(phone, code);
        await _smsMessaging.Send(new SmsCommand {Message = message.Item1, Recipients = new [] {message.Item2}});
        return new Result<bool>(true);
    }

    private async Task PostAsType(User user, DbTransaction transaction, DbConnection connection)
    {
        switch (user.Type)
        {
            case UserType.Artisan:
                await PostArtisan(user, transaction, connection);
                break;
            case UserType.Customer:
                await PostCustomer(user, transaction, connection);
                break;
            case UserType.Developer:
                break;
            case UserType.Administrator:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private async Task<User> SaveUser(UserCommand command)
    {
        var user = User.Create();
        var passes = await Task.Run(() => Uow.Users.Register(command));
        await AssignFields(user, command);
        user.HasPassword(passes.Item1)
            .HasPasswordKey(passes.Item2);
        await Uow.Users.InsertAsync(user);
        return user;
    }
    
    private async Task<string> SaveRegistration(string primaryContact)
    {
        var verificationCode = RandomStringHelper.Generate(20, true);
        var registration = Registration.Create(primaryContact, verificationCode);
        await Uow.Registrations.InsertAsync(registration);
        return verificationCode;
    }
    
    private async Task PostArtisan(User user, DbTransaction transaction, DbConnection connection)
    {
        var userId = await Uow.Users.GetIdByPhone(user.Contact.PrimaryContact, transaction, connection);
        var artisan = Artisan.Create(userId)
            .WithBusinessName(user.DisplayName)
            .WithDescription("")
            .CreatedOn();
        await Uow.Artisans.InsertAsync(artisan);
        await Uow.SaveChangesAsync();
    }
    
    private async Task PostCustomer(User user, DbTransaction transaction, DbConnection connection)
    {
        var userId = await Uow.Users.GetIdByPhone(user.Contact.PrimaryContact, transaction, connection);
        var customer = Customer.Create(userId);
        await Uow.Customers.InsertAsync(customer);
        await Uow.SaveChangesAsync();
    }
    
    public async Task<Result<bool>> HardDelete(string phone)
    {
        var registration = await Uow.Registrations.GetByPhone(phone);
        if (registration is null) return new Result<bool>(new InvalidIdException($"Registration with phone: {phone} does not exist"));
        await Uow.Registrations.DeleteAsync(registration, new CancellationToken());
        await Uow.SaveChangesAsync();
        return true;
    }

    private async Task AssignFields(User user, UserCommand command)
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
            .OfType(command.Type)
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