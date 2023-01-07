namespace AMP.Processors.Processors
{
    public enum LookupType
    {
        Artisan,
        Customer,
        Dispute,
        Order,
        Payment,
        Rating,
        Service,
        User,
        ArtisansServicess
    }

    [Processor]
    public class LookupProcessor : ProcessorBase
    {
        public LookupProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache) : base(uow, mapper, cache)
        {
        }

        public async Task<List<Lookup>> GetLookup(LookupType type)
        {
            return type switch
            {
                LookupType.Artisan => await Get(LookupType.Artisan, Uow.Artisans.GetLookupAsync),
                LookupType.Customer => await Get(LookupType.Customer, Uow.Customers.GetLookupAsync),
                LookupType.Dispute => await Get(LookupType.Dispute, Uow.Disputes.GetLookupAsync),
                LookupType.Order => await Get(LookupType.Order, Uow.Orders.GetLookupAsync),
                LookupType.Payment => await Get(LookupType.Payment, Uow.Payments.GetLookupAsync),
                LookupType.Rating => await Get(LookupType.Rating, Uow.Ratings.GetLookupAsync),
                LookupType.Service => await Get(LookupType.Service, Uow.Services.GetLookupAsync),
                LookupType.User => await Get(LookupType.User, Uow.Users.GetLookupAsync),
                LookupType.ArtisansServicess => await Get(LookupType.ArtisansServicess, Uow.Services.GetAvailableServices),
                _ => new List<Lookup>()
            };
        }

        public async Task<List<Lookup>> GetOpenOrdersLookup(string userId) 
            => await Uow.Orders.GetOpenOrdersLookup(userId);

        private async Task<List<Lookup>> Get(LookupType type, Func<Task<List<Lookup>>> getter)
        {
            var cacheKey = string.Join("", type.ToString(), "lookup");
            var cache = Cache.Get<List<Lookup>>(cacheKey);
            if (cache != null) return cache;
            var lookup = await getter.Invoke();
            Cache.Set(cacheKey, lookup); 
            return lookup;
        }
    }
}