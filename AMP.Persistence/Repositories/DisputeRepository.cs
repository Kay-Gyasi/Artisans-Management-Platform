using System.Data;
using AMP.Processors.Exceptions;
using AMP.Processors.Repositories.Base;

namespace AMP.Persistence.Repositories
{
    [Repository]
    public class DisputeRepository : RepositoryBase<Disputes>, IDisputeRepository
    {
        private readonly IDapperContext _dapperContext;

        public DisputeRepository(AmpDbContext context, ILogger<Disputes> logger,
            IDapperContext dapperContext) : base(context, logger)
        {
            _dapperContext = dapperContext;
        }
        
        public async Task DeleteAsync(string id)
        {
            var rows = await _dapperContext.Execute(
                "UPDATE Disputes SET EntityStatus = 'Deleted', DateModified = GETDATE() " +
                $"WHERE Id = '{id}'",
                null,
                CommandType.Text);
            if (rows == 0) throw new InvalidIdException($"Dispute with id: {id} does not exist");
        }

        public async Task<PaginatedList<Disputes>> GetUserPage(PaginatedCommand paginated, string userId, CancellationToken cancellationToken)
        {
            var customerId = await _dapperContext.GetAsync<string>
            ($"SELECT Id FROM Customers WHERE UserId = '{userId}'"
                    .AddBaseFilterToWhereClause(), null,
                CommandType.Text);

            var whereQueryable = GetBaseQuery().Where(x => x.CustomerId == customerId)
                .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));

            return await whereQueryable.BuildPage(paginated, cancellationToken);
        }

        public async Task<int> OpenDisputeCount(string userId) =>
            await Context.Set<Disputes>().Where(x => x.Customer.UserId == userId 
                                            && x.Status == DisputeStatus.Open).CountAsync();

        public override IQueryable<Disputes> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.Order)
                .Include(x => x.Customer);
        }

        public DbContext GetDbContext() => Context;

        protected override Expression<Func<Disputes, bool>> GetSearchCondition(string search)
        {
            return x => x.Status != DisputeStatus.Resolved;
        }
    }
}