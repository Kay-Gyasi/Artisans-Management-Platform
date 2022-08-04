using System.Linq;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    [Repository]
    public class RatingRepository : RepositoryBase<Ratings>, IRatingRepository
    {
        public RatingRepository(AmpDbContext context, ILogger<Ratings> logger) : base(context, logger)
        {
        }

        public int GetCount(int artisanId)
        {
            return GetBaseQuery().Count(x => x.ArtisanId == artisanId);
        }

        public double GetRating(int artisanId)
        {
            int votes = 0;
            var ratings = GetBaseQuery().Where(x => x.ArtisanId == artisanId);
            foreach (var rating in ratings)
            {
                votes += rating.Votes;
            }

            var rate = votes / (double) ratings.Count();
            return rate > 0? rate : 0;
        }

        public override IQueryable<Ratings> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.Artisan)
                .Include(x => x.Customer);
        }

    }
}