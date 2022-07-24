using AMP.Domain.Entities;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    public class ServiceRepository : RepositoryBase<Services>, IServiceRepository
    {
        protected ServiceRepository(AmpDbContext context, ILogger<Services> logger) : base(context, logger)
        {
        }
    }
}