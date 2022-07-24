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
    public class CustomerProcessor : ProcessorBase
    {
        public CustomerProcessor(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }

        public async Task<int> Save(CustomerCommand command)
        {
            var isNew = command.Id == 0;

            Customers customer;
            if (isNew)
            {
                customer = Customers.Create(command.UserId)
                    .CreatedOn(DateTime.UtcNow);
                await _uow.Customers.InsertAsync(customer);
                await _uow.SaveChangesAsync();
                return customer.Id;
            }

            customer = await _uow.Customers.GetAsync(command.Id);
            customer.ForUserId(command.UserId);
            await _uow.Customers.UpdateAsync(customer);
            await _uow.SaveChangesAsync();
            return customer.Id;
        }

        public async Task<PaginatedList<CustomerPageDto>> GetPage(PaginatedCommand command)
        {
            var page = await _uow.Customers.GetPage(command, new CancellationToken());
            return _mapper.Map<PaginatedList<CustomerPageDto>>(page);
        }

        public async Task<CustomerDto> Get(int id)
        {
            return _mapper.Map<CustomerDto>(await _uow.Customers.GetAsync(id));
        }

        public async Task Delete(int id)
        {
            var artisan = await _uow.Customers.GetAsync(id);
            if (artisan != null) await _uow.Customers.DeleteAsync(artisan, new CancellationToken());
            await _uow.SaveChangesAsync();
        }

    }
}