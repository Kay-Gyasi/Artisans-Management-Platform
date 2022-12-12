﻿using AMP.Processors.Exceptions;

namespace AMP.Processors.Processors
{

    [Processor]
    public class CustomerProcessor : ProcessorBase
    {
        private const string LookupCacheKey = "Customerlookup";

        public CustomerProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache) : base(uow, mapper, cache)
        {
        }

        public async Task<Result<string>> Save(CustomerCommand command)
        {
            var isNew = string.IsNullOrEmpty(command.Id);

            Customers customer;
            if (isNew)
            {
                customer = Customers.Create(command.UserId);
                Cache.Remove(LookupCacheKey);
                await Uow.Customers.InsertAsync(customer);
                await Uow.SaveChangesAsync();
                return new Result<string>(customer.Id);
            }

            customer = await Uow.Customers.GetAsync(command.Id);
            if (customer is null)
                return new Result<string>(
                    new InvalidIdException($"Customer with id: {customer.Id} does not exist"));
            customer.ForUserId(command.UserId);
            Cache.Remove(LookupCacheKey);
            await Uow.Customers.UpdateAsync(customer);
            await Uow.SaveChangesAsync();
            return new Result<string>(customer.Id);
        }

        public async Task<PaginatedList<CustomerPageDto>> GetPage(PaginatedCommand command)
        {
            var page = await Uow.Customers.GetPage(command, new CancellationToken());
            return Mapper.Map<PaginatedList<CustomerPageDto>>(page);
        }

        public async Task<Result<CustomerDto>> Get(string id)
        {
            var customerDto = Mapper.Map<CustomerDto>(await Uow.Customers.GetAsync(id));
            if (customerDto is null)
                return new Result<CustomerDto>(
                    new InvalidIdException($"Customer with id: {id} does not exist"));
            return new Result<CustomerDto>(customerDto);
        }

        public async Task<Result<CustomerDto>> GetByUserId(string userId)
        {
            var customer = Mapper.Map<CustomerDto>(await Uow.Customers.GetByUserIdAsync(userId));
            if (customer is null)
                return new Result<CustomerDto>(
                    new InvalidIdException($"Customer with userId: {userId} does not exist"));
            return new Result<CustomerDto>(customer);
        }

        public async Task<Result<bool>> Delete(string id)
        {
            try
            {
                await Uow.Customers.DeleteAsync(id);
                Cache.Remove(LookupCacheKey);
                return new Result<bool>(true);
            }
            catch (Exception e)
            {
                return new Result<bool>(e);
            }
        }

    }
}