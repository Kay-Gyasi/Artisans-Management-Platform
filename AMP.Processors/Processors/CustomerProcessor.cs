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
    public class CustomerProcessor : ProcessorBase
    {
        private const string LookupCacheKey = "Customerlookup";

        public CustomerProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache) : base(uow, mapper, cache)
        {
        }

        public async Task<string> Save(CustomerCommand command)
        {
            var isNew = string.IsNullOrEmpty(command.Id);

            Customers customer;
            if (isNew)
            {
                customer = Customers.Create(command.UserId)
                    .CreatedOn();
                Cache.Remove(LookupCacheKey);
                await Uow.Customers.InsertAsync(customer);
                await Uow.SaveChangesAsync();
                return customer.Id;
            }

            customer = await Uow.Customers.GetAsync(command.Id);
            customer.ForUserId(command.UserId);
            Cache.Remove(LookupCacheKey);
            await Uow.Customers.UpdateAsync(customer);
            await Uow.SaveChangesAsync();
            return customer.Id;
        }

        public async Task<PaginatedList<CustomerPageDto>> GetPage(PaginatedCommand command)
        {
            var page = await Uow.Customers.GetPage(command, new CancellationToken());
            return Mapper.Map<PaginatedList<CustomerPageDto>>(page);
        }

        public async Task<CustomerDto> Get(string id)
        {
            return Mapper.Map<CustomerDto>(await Uow.Customers.GetAsync(id));
        }

        public async Task<CustomerDto> GetByUserId(string userId)
        {
            return Mapper.Map<CustomerDto>(await Uow.Customers.GetByUserIdAsync(userId));
        }

        public async Task Delete(string id)
        {
            var customer = await Uow.Customers.GetAsync(id);
            Cache.Remove(LookupCacheKey);
            if (customer != null) await Uow.Customers.SoftDeleteAsync(customer);
            await Uow.SaveChangesAsync();
        }

    }
}