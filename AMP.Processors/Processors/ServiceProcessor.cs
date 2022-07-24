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
    public class ServiceProcessor : ProcessorBase
    {
        public ServiceProcessor(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }

        public async Task<int> Save(ServiceCommand command)
        {
            var isNew = command.Id == 0;

            Services service;
            if (isNew)
            {
                service = Services.Create(command.Name, command.Description)
                    .CreatedOn(DateTime.UtcNow);
                await _uow.Services.InsertAsync(service);
                await _uow.SaveChangesAsync();
                return service.Id;
            }

            service = await _uow.Services.GetAsync(command.Id);
            service.WithDescription(command.Description)
                .WithName(command.Name);
            await _uow.Services.UpdateAsync(service);
            await _uow.SaveChangesAsync();
            return service.Id;
        }

        public async Task<PaginatedList<ServicePageDto>> GetPage(PaginatedCommand command)
        {
            var page = await _uow.Services.GetPage(command, new CancellationToken());
            return _mapper.Map<PaginatedList<ServicePageDto>>(page);
        }

        public async Task<ServiceDto> Get(int id)
        {
            return _mapper.Map<ServiceDto>(await _uow.Services.GetAsync(id));
        }

        public async Task Delete(int id)
        {
            var artisan = await _uow.Services.GetAsync(id);
            if (artisan != null) await _uow.Services.DeleteAsync(artisan, new CancellationToken());
            await _uow.SaveChangesAsync();
        }
    }
}