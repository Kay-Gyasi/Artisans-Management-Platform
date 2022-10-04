using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using AMP.Shared.Domain.Models;
using AMP.Shared.Persistence;
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

        public async Task OverridePreviousRating(string customerId, string artisanId)
        {
            var rate = await GetBaseQuery().FirstOrDefaultAsync(x => x.ArtisanId == artisanId && x.CustomerId == customerId);
            if (rate != null) await SoftDeleteAsync(rate);
        }

        public int GetCount(string artisanId)
        {
            return GetBaseQuery().Count(x => x.ArtisanId == artisanId);
        }

        public double GetRating(string artisanId)
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
                .Include(x => x.Customer)
                .ThenInclude(x => x.User);
        }

        public async Task<PaginatedList<Ratings>> GetArtisanRatingPage(PaginatedCommand paginated, string userId, 
            CancellationToken cancellationToken)
        {
            IQueryable<Ratings> whereQueryable;
            if (string.IsNullOrEmpty(paginated.OtherJson))
            {
                whereQueryable = base.GetBaseQuery().Where(x => x.Artisan.UserId == userId)
                    .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));
            }
            else
            {
                whereQueryable = GetBaseQuery().Where(x => x.ArtisanId == paginated.OtherJson)
                    .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));
            }

            var orders = await BuildPage(whereQueryable, paginated, cancellationToken);
            return orders;
        }


        private static async Task<PaginatedList<Ratings>> BuildPage(IQueryable<Ratings> whereQueryable, PaginatedCommand paginated,
            CancellationToken cancellationToken)
        {
            var pagedModel = await whereQueryable.PageBy(x => paginated.Take, paginated)
                .ToListAsync(cancellationToken);

            var totalRecords = await whereQueryable.CountAsync(cancellationToken: cancellationToken);


            return new PaginatedList<Ratings>(data: pagedModel,
                totalCount: totalRecords,
                currentPage: paginated.PageNumber,
                pageSize: paginated.PageSize);
        }
    }
}