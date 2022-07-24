using AMP.Domain.Entities;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    public class RatingRepository : RepositoryBase<Ratings>, IRatingRepository
    {
        protected RatingRepository(AmpDbContext context, ILogger<Ratings> logger) : base(context, logger)
        {
        }
    }
}