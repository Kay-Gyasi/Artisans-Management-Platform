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
using Microsoft.Extensions.Caching.Memory;

namespace AMP.Processors.Processors
{
    [Processor]
    public class DisputeProcessor : ProcessorBase
    {
        private const string LookupCacheKey = "Disputelookup";

        public DisputeProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache) : base(uow, mapper, cache)
        {
        }

        public async Task<string> Save(DisputeCommand command, string userId)
        {
            var isNew = string.IsNullOrEmpty(command.Id);
            command.CustomerId = await _uow.Customers.GetCustomerId(userId);

            Disputes dispute;
            if (isNew)
            {
                dispute = Disputes.Create(command.CustomerId, command.OrderId)
                    .CreatedOn(DateTime.UtcNow);
                AssignFields(dispute, command, true);
                _cache.Remove(LookupCacheKey);
                await _uow.Disputes.InsertAsync(dispute);
                await _uow.SaveChangesAsync();
                return dispute.Id;
            }

            dispute = await _uow.Disputes.GetAsync(command.Id);
            AssignFields(dispute, command);
            _cache.Remove(LookupCacheKey);
            await _uow.Disputes.UpdateAsync(dispute);
            await _uow.SaveChangesAsync();
            return dispute.Id;
        }

        public async Task<PaginatedList<DisputePageDto>> GetPage(PaginatedCommand command)
        {
            var page = await _uow.Disputes.GetPage(command, new CancellationToken());
            return _mapper.Map<PaginatedList<DisputePageDto>>(page);
        }

        public async Task<DisputeDto> Get(string id)
        {
            return _mapper.Map<DisputeDto>(await _uow.Disputes.GetAsync(id));
        }
        
        public async Task<DisputeCount> GetOpenDisputeCount(string userId)
        {
            return new DisputeCount
            {
                Count = await _uow.Disputes.OpenDisputeCount(userId)
            };
        }

        public async Task Delete(string id)
        {
            var dispute = await _uow.Disputes.GetAsync(id);
            _cache.Remove(LookupCacheKey);
            if (dispute != null) await _uow.Disputes.SoftDeleteAsync(dispute);
        }

        private static void AssignFields(Disputes dispute, DisputeCommand command, bool isNew = false)
        {
            dispute.WithDetails(command.Details)
                .WithStatus(command.Status);

            if (!isNew)
                dispute.ByCustomerWithId(command.CustomerId)
                    .AgainstOrderWithId(command.OrderId);
        }
    }
}