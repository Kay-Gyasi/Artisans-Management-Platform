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
    public class ServiceProcessor : ProcessorBase
    {
        private const string LookupCacheKey = "Servicelookup";

        public ServiceProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache) : base(uow, mapper, cache)
        {
        }

        public async Task<string> Save(ServiceCommand command)
        {
            var isNew = string.IsNullOrEmpty(command.Id);

            Services service;
            if (isNew)
            {
                service = Services.Create(command.Name, command.Description)
                    .CreatedOn();
                Cache.Remove(LookupCacheKey);
                await Uow.Services.InsertAsync(service);
                await Uow.SaveChangesAsync();
                return service.Id;
            }

            service = await Uow.Services.GetAsync(command.Id);
            service.WithDescription(command.Description)
                .WithName(command.Name)
                .LastModifiedOn();
            Cache.Remove(LookupCacheKey);
            await Uow.Services.UpdateAsync(service);
            await Uow.SaveChangesAsync();
            return service.Id;
        }

        public async Task<PaginatedList<ServicePageDto>> GetPage(PaginatedCommand command)
        {
            var page = await Uow.Services.GetPage(command, new CancellationToken());
            return Mapper.Map<PaginatedList<ServicePageDto>>(page);
        }

        public async Task<ServiceDto> Get(string id)
        {
            return Mapper.Map<ServiceDto>(await Uow.Services.GetAsync(id));
        }

        public async Task Delete(string id)
        {
            var service = await Uow.Services.GetAsync(id);
            Cache.Remove(LookupCacheKey);
            if (service != null) await Uow.Services.SoftDeleteAsync(service);
            await Uow.SaveChangesAsync();
        }
    }
}