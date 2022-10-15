using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects;
using AMP.Processors.Commands;
using AMP.Processors.Messaging;
using AMP.Processors.Processors.Base;
using AMP.Processors.Processors.Helpers;
using AMP.Processors.Repositories.UoW;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace AMP.Processors.Processors;

[Processor]
public class RegistrationProcessor : ProcessorBase
{
    private readonly ISmsMessaging _smsMessaging;

    public RegistrationProcessor(IUnitOfWork uow, IMapper mapper, 
        IMemoryCache cache,
        ISmsMessaging smsMessaging) : base(uow, mapper, cache)
    {
        _smsMessaging = smsMessaging;
    }

    public async Task<string> Save(UserCommand command)
    {
        var userExists = await Uow.Users.Exists(command.Contact.PrimaryContact);
        if (userExists) return default;

        var user = await SaveUser(command);
        var code = await SaveRegistration(command.Contact.PrimaryContact);
        await Uow.SaveChangesAsync();
        await Task.WhenAll(SendVerificationLink(user.Contact.PrimaryContact, code), PostAsType(user));
        return user.Id;
    }

    public async Task<bool> VerifyUser(string phone, string code)
    {
        var verified = await Uow.Registrations.Crosscheck(phone, code);
        if (!verified) return false;

        await Uow.Registrations.Verify(phone, code);
        return true;
    }

    public async Task SendVerificationLink(string phone, string code)
    {
        if (string.IsNullOrEmpty(code))
        {
            code = RandomStringHelper.Generate(20, true);
            var registration = await Uow.Registrations.GetByPhone(phone);
            registration.HasVerificationCode(code);
            await Uow.SaveChangesAsync();
        }
        var message = MessageGenerator.SendVerificationLink(phone, code);
        await _smsMessaging.Send(new SmsCommand {Message = message.Item1, Recipients = new [] {message.Item2}});
    }
    
    private async Task PostAsType(Users user)
    {
        switch (user.Type)
        {
            case UserType.Artisan:
                await PostArtisan(user);
                break;
            case UserType.Customer:
                await PostCustomer(user);
                break;
            case UserType.Developer:
                break;
            case UserType.Administrator:
                break;
        }
    }

    private async Task<Users> SaveUser(UserCommand command)
    {
        var user = Users.Create()
            .CreatedOn();
        var passes = Uow.Users.Register(command);
        await AssignFields(user, command);
        user.HasPassword(passes.Item1)
            .HasPasswordKey(passes.Item2);
        //Cache.Remove(LookupCacheKey); (add this when you verify user)
        await Uow.Users.InsertAsync(user);
        return user;
    }
    
    private async Task<string> SaveRegistration(string primaryContact)
    {
        var verificationCode = RandomStringHelper.Generate(20, true);
        var registration = Registrations.Create(primaryContact, verificationCode);
        await Uow.Registrations.InsertAsync(registration);
        return verificationCode;
    }
    
    private async Task PostArtisan(Users user)
    {
        try
        {
            var userId = await Uow.Users.GetIdByPhone(user.Contact.PrimaryContact);
            var artisan = Artisans.Create(userId)
                .WithBusinessName(user.DisplayName)
                .WithDescription(string.Empty)
                .CreatedOn();
            await Uow.Artisans.InsertAsync(artisan);
        }
        catch (Exception)
        {
            var userId = await Uow.Users.GetIdByPhone(user.Contact.PrimaryContact);
            var deleted = await Uow.Users.GetAsync(userId);
            await Uow.Users.DeleteAsync(deleted, new CancellationToken());
        }
        finally
        {
            await Uow.SaveChangesAsync();
        }
    }
    
    private async Task PostCustomer(Users user)
    {
        try
        {
            var userId = await Uow.Users.GetIdByPhone(user.Contact.PrimaryContact);
            var customer = Customers.Create(userId)
                .CreatedOn();
            await Uow.Customers.InsertAsync(customer);
        }
        catch (Exception)
        {
            var userId = await Uow.Users.GetIdByPhone(user.Contact.PrimaryContact);
            var deleted = await Uow.Users.GetAsync(userId);
            await Uow.Users.DeleteAsync(deleted, new CancellationToken());
        }
        finally
        {
            await Uow.SaveChangesAsync();
        }
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
            .WithMomoNumber(command.MomoNumber ?? "")
            .IsSuspendedd(command.IsSuspended)
            .IsRemovedd(command.IsRemoved);
    }
}