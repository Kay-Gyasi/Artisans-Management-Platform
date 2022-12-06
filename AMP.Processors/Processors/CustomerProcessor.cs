using AMP.Processors.Exceptions;

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
            customer.ForUserId(command.UserId)
                .SetLastModified();
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
            var customerDto = Mapper.Map<CustomerDto>(await Uow.Customers.GetAsync(id));
            if (customerDto is null) throw new InvalidIdException($"Customer with id: {id} does not exist");
            return customerDto;
        }

        public async Task<CustomerDto> GetByUserId(string userId)
        {
            var customer = Mapper.Map<CustomerDto>(await Uow.Customers.GetByUserIdAsync(userId));
            if (customer is null) throw new InvalidIdException($"Customer with userId: {userId} does not exist");
            return customer;
        }

        public async Task Delete(string id)
        {
            await Uow.Customers.DeleteAsync(id);
            Cache.Remove(LookupCacheKey);
        }

    }
}