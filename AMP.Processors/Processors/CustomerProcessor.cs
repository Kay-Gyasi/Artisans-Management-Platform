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
                    .CreatedOn(DateTime.UtcNow);
                _cache.Remove(LookupCacheKey);
                await _uow.Customers.InsertAsync(customer);
                await _uow.SaveChangesAsync();
                return customer.Id;
            }

            customer = await _uow.Customers.GetAsync(command.Id);
            customer.ForUserId(command.UserId);
            _cache.Remove(LookupCacheKey);
            await _uow.Customers.UpdateAsync(customer);
            await _uow.SaveChangesAsync();
            return customer.Id;
        }

        public async Task<PaginatedList<CustomerPageDto>> GetPage(PaginatedCommand command)
        {
            var page = await _uow.Customers.GetPage(command, new CancellationToken());
            return _mapper.Map<PaginatedList<CustomerPageDto>>(page);
        }

        public async Task<CustomerDto> Get(string id)
        {
            return _mapper.Map<CustomerDto>(await _uow.Customers.GetAsync(id));
        }

        public async Task<CustomerDto> GetByUserId(string userId)
        {
            return _mapper.Map<CustomerDto>(await _uow.Customers.GetByUserIdAsync(userId));
        }

        public async Task Delete(string id)
        {
            var customer = await _uow.Customers.GetAsync(id);
            _cache.Remove(LookupCacheKey);
            if (customer != null) await _uow.Customers.SoftDeleteAsync(customer);
            await _uow.SaveChangesAsync();
        }

    }
}