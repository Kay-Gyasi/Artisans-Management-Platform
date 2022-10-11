using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects;
using AMP.Processors.Authentication;
using AMP.Processors.Commands;
using AMP.Processors.Dtos;
using AMP.Processors.Messaging;
using AMP.Processors.PageDtos;
using AMP.Processors.Processors.Base;
using AMP.Processors.Repositories.UoW;
using AMP.Shared.Domain.Models;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace AMP.Processors.Processors
{
    [Processor]
    public class UserProcessor : ProcessorBase
    {
        private const string LookupCacheKey = "Userlookup";

        private readonly IAuthService _authService;

        public UserProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache,
            IAuthService authService) : base(uow, mapper, cache)
        {
            _authService = authService;
        }

        public async Task<SigninResponse> Login(SigninCommand command)
        {
            var user = await Uow.Users.Authenticate(command);
            return user is null ? null : new SigninResponse { Token = _authService.GenerateToken(user) };
        }

        public async Task<SigninResponse> GetRefreshToken(string userId)
        {
            var user = await Uow.Users.GetAsync(userId);
            return user is null ? null : new SigninResponse { Token = _authService.GenerateToken(user) };
        }

        public async Task<string> Post(UserCommand command)
        {
            var userExists = await Uow.Users.Exists(command.Contact.PrimaryContact);
            if (userExists) return default;

            var user = Users.Create()
                .CreatedOn();
            var passes = Uow.Users.Register(command);
            await AssignFields(user, command);
            user.HasPassword(passes.Item1)
                .HasPasswordKey(passes.Item2);
            Cache.Remove(LookupCacheKey);
            await Uow.Users.InsertAsync(user);
            await Uow.SaveChangesAsync();

            await PostAsType(user);
            return user.Id;
        }

        public async Task<string> Update(UserCommand command)
        {
            var user = await Uow.Users.GetAsync(command.Id);
            await AssignFields(user, command);
            Cache.Remove(LookupCacheKey);
            await Uow.Users.UpdateAsync(user);
            await Uow.SaveChangesAsync();
            return user.Id;
        }

        public async Task<PaginatedList<UserPageDto>> GetPage(PaginatedCommand command)
        {
            var page = await Uow.Users.GetPage(command, new CancellationToken());
            return Mapper.Map<PaginatedList<UserPageDto>>(page);
        }

        public async Task<UserDto> Get(string id)
        {
            return Mapper.Map<UserDto>(await Uow.Users.GetAsync(id));
        }

        public async Task Delete(string id)
        {
            var user = await Uow.Users.GetAsync(id);
            Cache.Remove(LookupCacheKey);
            if (user != null) await Uow.Users.SoftDeleteAsync(user);
            await Uow.SaveChangesAsync();
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
                default:
                    break;
            }
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
            var list = command.Languages.Select(lang => lang.Name).ToList();
            var languages = await Uow.Languages.BuildLanguages(list);
            user.WithFirstName(command.FirstName)
                .WithFamilyName(command.FamilyName)
                .WithOtherName(command.OtherName)
                .SetDisplayName()
                .WithImageId(command.ImageId)
                .OfType(command.Type)
                .HasLevelOfEducation(command.LevelOfEducation)
                .WithContact(Contact.Create(command.Contact.PrimaryContact ?? "")
                    .WithPrimaryContact2(command.Contact.PrimaryContact2 ?? "")
                    .WithPrimaryContact3(command.Contact.PrimaryContact3 ?? "")
                    .WithEmailAddress(command.Contact.EmailAddress ?? ""))
                .WithAddress(Address.Create(command.Address.City, command.Address.StreetAddress)
                    .WithStreetAddress2(command.Address.StreetAddress2)
                    .FromTown(command.Address.Town)
                    .FromCountry(command.Address.Country))
                .Speaks(languages)
                .WithMomoNumber(command.MomoNumber)
                .IsSuspendedd(command.IsSuspended)
                .IsRemovedd(command.IsRemoved);
        }
    }
}