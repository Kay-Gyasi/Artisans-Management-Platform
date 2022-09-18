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
                _cache.Remove(LookupCacheKey);
                await _uow.Services.InsertAsync(service);
                await _uow.SaveChangesAsync();
                return service.Id;
            }

            service = await _uow.Services.GetAsync(command.Id);
            service.WithDescription(command.Description)
                .WithName(command.Name)
                .LastModifiedOn();
            _cache.Remove(LookupCacheKey);
            await _uow.Services.UpdateAsync(service);
            await _uow.SaveChangesAsync();
            return service.Id;
        }

        public async Task<PaginatedList<ServicePageDto>> GetPage(PaginatedCommand command)
        {
            var page = await _uow.Services.GetPage(command, new CancellationToken());
            return _mapper.Map<PaginatedList<ServicePageDto>>(page);
        }

        public async Task<ServiceDto> Get(string id)
        {
            return _mapper.Map<ServiceDto>(await _uow.Services.GetAsync(id));
        }

        public async Task Delete(string id)
        {
            var service = await _uow.Services.GetAsync(id);
            _cache.Remove(LookupCacheKey);
            if (service != null) await _uow.Services.SoftDeleteAsync(service);
            await _uow.SaveChangesAsync();
        }
    }
}