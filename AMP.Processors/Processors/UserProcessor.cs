﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects;
using AMP.Processors.Authentication;
using AMP.Processors.Commands;
using AMP.Processors.Dtos;
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
        private readonly IAuthService _authService;

        public UserProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache,
            IAuthService authService) : base(uow, mapper, cache)
        {
            _authService = authService;
        }

        public async Task<SigninResponse> Login(SigninCommand command)
        {
            var user = await _uow.Users.Authenticate(command);
            return user is null ? null : new SigninResponse { Token = _authService.GenerateToken(user) };
        }

        public async Task<int> Post(UserCommand command)
        {
            var userExists = await _uow.Users.Exists(command.Contact.EmailAddress);
            if (userExists) return default;

            var user = Users.Create()
                .CreatedOn(DateTime.UtcNow);
            var passes = _uow.Users.Register(command);
            await AssignFields(user, command);
            user.HasPassword(passes.Item1)
                .HasPasswordKey(passes.Item2);
            await _uow.Users.InsertAsync(user);
            await _uow.SaveChangesAsync();

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
            return user.Id;
        }

        public async Task<int> Save(UserCommand command)
        {
            var user = await _uow.Users.GetAsync(command.Id);
            await AssignFields(user, command);
            await _uow.Users.UpdateAsync(user);
            await _uow.SaveChangesAsync();
            return user.Id;
        }

        public async Task<PaginatedList<UserPageDto>> GetPage(PaginatedCommand command)
        {
            var page = await _uow.Users.GetPage(command, new CancellationToken());
            return _mapper.Map<PaginatedList<UserPageDto>>(page);
        }

        public async Task<UserDto> Get(int id)
        {
            return _mapper.Map<UserDto>(await _uow.Users.GetAsync(id));
        }

        public async Task Delete(int id)
        {
            var artisan = await _uow.Users.GetAsync(id);
            if (artisan != null) await _uow.Users.DeleteAsync(artisan, new CancellationToken());
            await _uow.SaveChangesAsync();
        }

        private async Task PostArtisan(Users user)
        {
            try
            {
                var userId = await _uow.Users.GetIdByEmail(user.Contact.EmailAddress);
                var artisan = Artisans.Create(userId)
                    .WithBusinessName(user.DisplayName)
                    .CreatedOn(DateTime.UtcNow);
                await _uow.Artisans.InsertAsync(artisan);
                await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                var userId = await _uow.Users.GetIdByEmail(user.Contact.EmailAddress);
                var deleted = await _uow.Users.GetAsync(userId);
                await _uow.Users.DeleteAsync(deleted, new CancellationToken());
            }
            
        }
        
        private async Task PostCustomer(Users user)
        {
            try
            {
                var userId = await _uow.Users.GetIdByEmail(user.Contact.EmailAddress);
                var customer = Customers.Create(userId)
                    .CreatedOn(DateTime.UtcNow);
                await _uow.Customers.InsertAsync(customer);
                await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                var userId = await _uow.Users.GetIdByEmail(user.Contact.EmailAddress);
                var deleted = await _uow.Users.GetAsync(userId);
                await _uow.Users.DeleteAsync(deleted, new CancellationToken());
            }
            
        }

        private async Task AssignFields(Users user, UserCommand command)
        {
            var lsit = command.Languages.Select(lang => lang.Name).ToList();
            var languages = await _uow.Languages.BuildLanguages(lsit);
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