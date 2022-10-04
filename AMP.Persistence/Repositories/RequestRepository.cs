using AMP.Domain.Entities;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.Extensions.Logging;

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