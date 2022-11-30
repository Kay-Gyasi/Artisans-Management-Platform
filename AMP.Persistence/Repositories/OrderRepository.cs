namespace AMP.Persistence.Repositories
{
    [Repository]
    public class OrderRepository : RepositoryBase<Orders>, IOrderRepository
    {
        public OrderRepository(AmpDbContext context, ILogger<Orders> logger) : base(context, logger)
        {
        }
        
        public Task<List<Lookup>> GetOpenOrdersLookup(string userId)
        {
            return base.GetBaseQuery().Where(x => x.Customer.UserId == userId && !x.IsComplete).Select(x => new Lookup()
                {
                    Id = x.Id,
                    Name = x.Description
                }).OrderBy(x => x.Name)
                .ToListAsync();
        }
      
        public async Task Complete(string orderId)
        {
            var order = await base.GetBaseQuery().FirstOrDefaultAsync(x => x.Id == orderId && x.IsArtisanComplete);
            if (order is null) return;
            order?.WithStatus(OrderStatus.Completed)
                .IsCompleted(true);
            order?.SetLastModified();
            await UpdateAsync(order);
        }
        
        public async Task ArtisanComplete(string orderId)
        {
            var order = await GetBaseQuery().FirstOrDefaultAsync(x => x.Id == orderId);
            order?.IsArtisanCompleted(true);
            order?.SetLastModified();
            await UpdateAsync(order);
        }

        public async Task AcceptRequest(string orderId)
        {
            var order = await GetBaseQuery().FirstOrDefaultAsync(x => x.Id == orderId);
            order?.RequestAccepted(true);
            order?.SetLastModified();
            await UpdateAsync(order);
        }
        
        public async Task CancelRequest(string orderId)
        {
            var order = await GetBaseQuery().FirstOrDefaultAsync(x => x.Id == orderId);
            order?.RequestAccepted(false);
            order?.SetLastModified();
            await UpdateAsync(order);
        }

        public async Task UnassignArtisan(string orderId)
        {
            var order = await GetBaseQuery().FirstOrDefaultAsync(x => x.Id == orderId);
            order?.ForArtisanWithId(null);
            order?.SetLastModified();
            await UpdateAsync(order);
        }

        public async Task AssignArtisan(string orderId, string artisanId)
        {
            var order = await GetBaseQuery().FirstOrDefaultAsync(x => x.Id == orderId);
            order?.ForArtisanWithId(artisanId);
            order?.SetLastModified();
            await UpdateAsync(order);

            await Context.Requests.AddAsync(Requests.Create(order?.CustomerId, artisanId, orderId));
        }

        public async Task<PaginatedList<Orders>> GetCustomerOrderPage(PaginatedCommand paginated,
            string userId, CancellationToken cancellationToken)
        {
            var whereQueryable = GetBaseQuery().Where(x => x.Status != OrderStatus.Completed && x.Customer.UserId == userId)
                .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));
            var orders = await whereQueryable.BuildPage(paginated, cancellationToken);
            return orders;
        }

        public async Task<PaginatedList<Orders>> GetOrderHistory(PaginatedCommand paginated,
            string userId, CancellationToken cancellationToken)
        {
            var whereQueryable = GetBaseQuery().Where(x => x.Status == OrderStatus.Completed && x.Customer.UserId == userId)
                .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));

            return await whereQueryable.BuildPage(paginated, cancellationToken);
        }

        public async Task<PaginatedList<Orders>> GetSchedule(PaginatedCommand paginated, string userId, 
            CancellationToken cancellationToken)
        {
            var whereQueryable = GetBaseQuery().Where(x => x.Artisan.UserId == userId 
                                                           && x.Status != OrderStatus.Completed
                                                           && x.IsRequestAccepted)
                .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search))
                .OrderBy(x => x.PreferredStartDate);

            return await whereQueryable.BuildPage(paginated, cancellationToken);
        }
        
        public async Task<PaginatedList<Orders>> GetRequests(PaginatedCommand paginated, string userId, 
            CancellationToken cancellationToken)
        {
            var whereQueryable = GetBaseQuery().Where(x => x.Artisan.UserId == userId 
                                                           && x.Status != OrderStatus.Completed
                                                           && !x.IsRequestAccepted)
                .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));

            return await whereQueryable.BuildPage(paginated, cancellationToken);
        }

        public async Task<PaginatedList<Orders>> GetWorkHistory(PaginatedCommand paginated, string userId
            , CancellationToken cancellationToken)
        {
            var whereQueryable = GetBaseQuery().Where(x => x.Artisan.UserId == userId && x.Status == OrderStatus.Completed)
                .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));

            return await whereQueryable.BuildPage(paginated, cancellationToken);
        }

        public async Task<int> GetCount(string artisanId)
        {
            return await Context.Set<Orders>()
                .AsNoTracking()
                .CountAsync(x => x.ArtisanId == artisanId);
        }

        public override IQueryable<Orders> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.WorkAddress)
                .Include(x => x.Artisan)
                .Include(x => x.Service)
                .Include(x => x.Customer)
                .ThenInclude(a => a.User);
        }

        // NOTE:: .StartsWith() => search%, .Contains() => %search%
        protected override Expression<Func<Orders, bool>> GetSearchCondition(string search)
        {
            
            return x => x.Description.ToLower().Contains(search.ToLower())
                        || x.Service.Name == search
                        || x.WorkAddress.City.ToLower().Contains(search.ToLower())
                        || x.WorkAddress.StreetAddress.ToLower().Contains(search.ToLower())
                        || x.WorkAddress.StreetAddress2.ToLower().Contains(search.ToLower());
            
        }

        public override Task<List<Lookup>> GetLookupAsync()
        {
            return GetBaseQuery()
                .AsNoTracking()
                .Select(x => new Lookup()
            {
                Id = x.Id,
                Name = x.Description
            }).OrderBy(x => x.Name)
                .ToListAsync();
        }
        public async Task SetCost(SetCostCommand costCommand)
        {
            var order = await GetBaseQuery().FirstOrDefaultAsync(x => x.Id == costCommand.OrderId);
            order?.WithCost(costCommand.Cost);
            order?.SetLastModified();
        }
    }
}