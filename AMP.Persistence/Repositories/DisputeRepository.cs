using AMP.Domain.Entities;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    public class DisputeRepository : RepositoryBase<Disputes>, IDisputeRepository
    {
        protected DisputeRepository(AmpDbContext context, ILogger<Disputes> logger) : base(context, logger)
        {
        }
    }
}