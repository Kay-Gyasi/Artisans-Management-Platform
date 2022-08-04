using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
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

        public async Task<int> Save(UserCommand command)
        {
            var isNew = command.Id == 0;

            Users user;
            if (isNew)
            {
                user = Users.Create()
                    .CreatedOn(DateTime.UtcNow);
                var passes =  _uow.Users.Register(command);
                await AssignFields(user, command);
                user.HasPassword(passes.Item1)
                    .HasPasswordKey(passes.Item2);
                await _uow.Users.InsertAsync(user);
                await _uow.SaveChangesAsync();
                return user.Id;
            }

            user = await _uow.Users.GetAsync(command.Id);
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

        private async Task AssignFields(Users user, UserCommand command)
        {
            var lsit = new List<string>();
            foreach (var lang in command.Languages)
            {
                lsit.Add(lang.Name);
            }
            var languages = await _uow.Languages.BuildLanguages(lsit);
            user.WithFirstName(command.FirstName)
                .WithFamilyName(command.FamilyName)
                .WithOtherName(command.OtherName)
                .SetDisplayName()
                .WithImageUrl(command.ImageUrl)
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