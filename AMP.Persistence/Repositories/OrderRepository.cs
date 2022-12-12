using System.Data;
using AMP.Processors.Exceptions;
using AMP.Processors.Repositories.Base;

namespace AMP.Persistence.Repositories
{
    [Repository]
    public class OrderRepository : RepositoryBase<Orders>, IOrderRepository
    {
        private readonly IDapperContext _dapperContext;

        public OrderRepository(AmpDbContext context, ILogger<Orders> logger,
            IDapperContext dapperContext) : base(context, logger)
        {
            _dapperContext = dapperContext;
        }

        public async Task DeleteAsync(string id)
        {
            var rows = await _dapperContext.Execute(
                "UPDATE Orders SET EntityStatus = 'Deleted', DateModified = GETDATE() " +
                $"WHERE Id = '{id}'",
                null,
                CommandType.Text);
            if (rows == 0) throw new InvalidIdException($"Order with id: {id} does not exist");
        }

        public Task<List<Lookup>> GetOpenOrdersLookup(string userId)
        {
            return base.GetBaseQuery().Where(x => x.Customer.UserId == userId && !x.IsComplete)
                .Select(x => new Lookup()
                {
                    Id = x.Id,
                    Name = x.Description
                }).OrderBy(x => x.Name)
                .ToListAsync();
        }
      
        public async Task Complete(string orderId)
        {
            var rows = await _dapperContext.Execute(
                $"UPDATE Orders SET Status = 'Completed', IsComplete = 1, DateModified = GETDATE() " +
                $"WHERE Id = '{orderId}' AND IsArtisanComplete = 1",
                null, CommandType.Text);
            if (rows == 0) throw new InvalidIdException($"Order with id: {orderId} does not exist");
        }
        
        public async Task ArtisanComplete(string orderId)
        {
            var rows = await _dapperContext.Execute(
                $"UPDATE Orders SET IsArtisanComplete = 1, DateModified = GETDATE() WHERE Id = '{orderId}'",
                null, CommandType.Text);
            if (rows == 0) throw new InvalidIdException($"Order with id: {orderId} does not exist");
        }

        public async Task AcceptRequest(string orderId)
        {
            var rows = await _dapperContext.Execute(
                $"UPDATE Orders SET IsRequestAccepted = 1, DateModified = GETDATE() WHERE Id = '{orderId}'",
                null, CommandType.Text);
            if (rows == 0) throw new InvalidIdException($"Order with id: {orderId} does not exist");
        }
        
        public async Task CancelRequest(string orderId)
        {
            var rows = await _dapperContext.Execute(
                $"UPDATE Orders SET IsRequestAccepted = 0, DateModified = GETDATE() WHERE Id = '{orderId}'",
                null, CommandType.Text);
            if (rows == 0) throw new InvalidIdException($"Order with id: {orderId} does not exist");
        }

        public async Task UnassignArtisan(string orderId)
        {
            var rows = await _dapperContext.Execute(
                $"UPDATE Orders SET ArtisanId = NULL, DateModified = GETDATE() WHERE Id = '{orderId}'",
             null, CommandType.Text);
            if (rows == 0) throw new InvalidIdException($"Order with id: {orderId} does not exist");
        }

        public async Task AssignArtisan(string orderId, string artisanId)
        {
            var order = await base.GetBaseQuery().FirstOrDefaultAsync(x => x.Id == orderId);
            if (order is null) throw new InvalidIdException($"Order with id: {orderId} does not exist");
            order.ForArtisanWithId(artisanId);
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

        public DbContext GetDbContext() => Context;

        public override IQueryable<Orders> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.WorkAddress)
                .Include(x => x.Artisan)
                .Include(x => x.Service)
                .Include(x => x.Customer)
                .ThenInclude(a => a.User);
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
            var rows = await _dapperContext.Execute(
                $"UPDATE Orders SET Cost = {costCommand.Cost}, DateModified = GETDATE() WHERE Id = '{costCommand.OrderId}'",
                null, CommandType.Text);
            if (rows == 0) throw new InvalidIdException($"Order with id: {costCommand.OrderId} does not exist");
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
    }
}