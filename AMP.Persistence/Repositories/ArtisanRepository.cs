using AMP.Domain.Entities;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    public class ArtisanRepository : RepositoryBase<Artisans>, IArtisanRepository
    {
        protected ArtisanRepository(AmpDbContext context, ILogger<Artisans> logger) : base(context, logger)
        {
        }
    }
}