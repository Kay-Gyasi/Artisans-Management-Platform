﻿using System;
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
    public class DisputeProcessor : ProcessorBase
    {
        public DisputeProcessor(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }

        public async Task<int> Save(DisputeCommand command)
        {
            var isNew = command.Id == 0;

            Disputes dispute;
            if (isNew)
            {
                dispute = Disputes.Create(command.CustomerId, command.ArtisanId)
                    .CreatedOn(DateTime.UtcNow);
                AssignFields(dispute, command, true);
                await _uow.Disputes.InsertAsync(dispute);
                await _uow.SaveChangesAsync();
                return dispute.Id;
            }

            dispute = await _uow.Disputes.GetAsync(command.Id);
            AssignFields(dispute, command);
            await _uow.Disputes.UpdateAsync(dispute);
            await _uow.SaveChangesAsync();
            return dispute.Id;
        }

        public async Task<PaginatedList<DisputePageDto>> GetPage(PaginatedCommand command)
        {
            var page = await _uow.Disputes.GetPage(command, new CancellationToken());
            return _mapper.Map<PaginatedList<DisputePageDto>>(page);
        }

        public async Task<DisputeDto> Get(int id)
        {
            return _mapper.Map<DisputeDto>(await _uow.Disputes.GetAsync(id));
        }

        public async Task Delete(int id)
        {
            var dispute = await _uow.Disputes.GetAsync(id);
            if (dispute != null) await _uow.Disputes.DeleteAsync(dispute, new CancellationToken());
        }

        private void AssignFields(Disputes dispute, DisputeCommand command, bool isNew = false)
        {
            dispute.WithDetails(command.Details)
                .WithStatus(command.Status);

            if (!isNew)
                dispute.ByCustomerWithId(command.CustomerId)
                    .AgainstArtisanWithId(command.ArtisanId);
        }
    }
}