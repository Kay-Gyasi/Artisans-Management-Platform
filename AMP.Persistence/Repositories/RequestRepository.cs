namespace AMP.Persistence.Repositories
{
    [Repository]
    public class RequestRepository : RepositoryBase<Requests>, IRequestRepository
    {
        public RequestRepository(AmpDbContext context, ILogger<Requests> logger) : base(context, logger)
        {
        }
    }
}