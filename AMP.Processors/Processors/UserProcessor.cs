using System;
using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Commands;
using AMP.Processors.Dtos;
using AMP.Processors.PageDtos;
using AMP.Processors.Processors.Base;
using AMP.Processors.Repositories.UoW;
using AMP.Shared.Domain.Models;
using AutoMapper;

namespace AMP.Processors.Processors
{
    [Processor]
    public class UserProcessor : ProcessorBase
    {
        public UserProcessor(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }

        public async Task<int> Save(UserCommand command)
        {
            var isNew = command.Id == 0;

            Users user;
            if (isNew)
            {
                user = Users.Create(command.UserNo)
                    .CreatedOn(DateTime.UtcNow);
                AssignFields(user, command, true);
                await _uow.Users.InsertAsync(user);
                await _uow.SaveChangesAsync();
                return user.Id;
            }

            user = await _uow.Users.GetAsync(command.Id);
            AssignFields(user, command);
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

        private void AssignFields(Users user, UserCommand command, bool isNew = false)
        {
            user.WithFirstName(command.FirstName)
                .WithFamilyName(command.FamilyName)
                .WithOtherName(command.OtherName)
                .SetDisplayName()
                .WithImageUrl(command.ImageUrl)
                .OfType(command.Type)
                .HasLevelOfEducation(command.LevelOfEducation)
                .WithContact(command.Contact)
                .WithAddress(command.Address)
                .Speaks(command.Languages)
                .WithMomoNumber(command.MomoNumber)
                .IsSuspendedd(command.IsSuspended)
                .IsRemovedd(command.IsRemoved);
            if (!isNew) user.ForUserWithNo(command.UserNo);
        }
    }
}