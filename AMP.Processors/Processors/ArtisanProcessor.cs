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
    public class ArtisanProcessor : ProcessorBase
    {
        public ArtisanProcessor(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }

        public async Task<int> Save(ArtisanCommand command)
        {
            var isNew = command.Id == 0;
            Artisans artisan;

            if (isNew)
            {
                artisan = Artisans.Create(command.UserId)
                    .CreatedOn(DateTime.UtcNow);
                AssignFields(artisan, command, true);
                await _uow.Artisans.InsertAsync(artisan);
                await _uow.SaveChangesAsync();
                return artisan.Id;
            }

            artisan = await _uow.Artisans.GetAsync(command.Id);
            AssignFields(artisan, command);
            await _uow.Artisans.UpdateAsync(artisan);
            await _uow.SaveChangesAsync();
            return artisan.Id;
        }

        public async Task<PaginatedList<ArtisanPageDto>> GetPage(PaginatedCommand command)
        {
            var page = await _uow.Artisans.GetPage(command, new CancellationToken());
            return _mapper.Map<PaginatedList<ArtisanPageDto>>(page);
        }

        public async Task<ArtisanDto> Get(int id)
        {
            return _mapper.Map<ArtisanDto>(await _uow.Artisans.GetAsync(id));
        }

        public async Task Delete(int id)
        {
            var artisan = await _uow.Artisans.GetAsync(id);
            if (artisan != null) await _uow.Artisans.DeleteAsync(artisan, new CancellationToken());
            await _uow.SaveChangesAsync();
        }

        private void AssignFields(Artisans artisan, ArtisanCommand command, bool isNew = false)
        {
            artisan.WithBusinessName(command.BusinessName)
                .WithDescription(command.Description)
                .IsVerifiedd(command.IsVerified)
                .IsApprovedd(command.IsApproved);
            if (!isNew) artisan.ForUserId(command.UserId);
        }
    }
}