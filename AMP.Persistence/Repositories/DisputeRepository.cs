namespace AMP.Persistence.Repositories
{
    [Repository]
    public class DisputeRepository : RepositoryBase<Disputes>, IDisputeRepository
    {
        private readonly AmpDbContext _context;

        public DisputeRepository(AmpDbContext context, ILogger<Disputes> logger) : base(context, logger)
        {
            _context = context;
        }

        public async Task<PaginatedList<Disputes>> GetUserPage(PaginatedCommand paginated, string userId, CancellationToken cancellationToken)
        {
            var customerId = (await _context.Customers.FirstOrDefaultAsync(x => x.UserId == userId, 
                cancellationToken: cancellationToken)).Id;
            var whereQueryable = GetBaseQuery().Where(x => x.CustomerId == customerId)
                .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));

            return await whereQueryable.BuildPage(paginated, cancellationToken);
        }

        public async Task<int> OpenDisputeCount(string userId) =>
            await GetBaseQuery().Where(x => x.Customer.UserId == userId 
                                            && x.Status == DisputeStatus.Open).CountAsync();

        public override IQueryable<Disputes> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.Order)
                .Include(x => x.Customer);
        }

        protected override Expression<Func<Disputes, bool>> GetSearchCondition(string search)
        {
            return x => x.Status != DisputeStatus.Resolved;
        }
    }
}