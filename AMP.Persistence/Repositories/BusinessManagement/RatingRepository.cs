using AMP.Domain.Entities.BusinessManagement;
using AMP.Processors.Repositories.BusinessManagement;

namespace AMP.Persistence.Repositories.BusinessManagement
{
    [Repository]
    public class RatingRepository : RepositoryBase<Rating>, IRatingRepository
    {
        public RatingRepository(AmpDbContext context, ILogger<Rating> logger) : base(context, logger)
        {
        }

        public async Task DeletePreviousRatingForSameArtisan(string customerId, string artisanId)
        {
            var rate = await GetBaseQuery().FirstOrDefaultAsync(x => x.ArtisanId == artisanId && x.CustomerId == customerId);
            if (rate != null) await SoftDeleteAsync(rate);
        }

        public int GetCount(string artisanId) 
            => GetBaseQuery().Count(x => x.ArtisanId == artisanId);

        public double GetRating(string artisanId)
        {
            var votes = 0;
            var ratings = GetBaseQuery().Where(x => x.ArtisanId == artisanId);
            foreach (var rating in ratings)
            {
                votes += rating.Votes;
            }

            var rate = votes / (double) ratings.Count();
            return rate > 0? rate : 0;
        }

        public override IQueryable<Rating> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.Artisan)
                .Include(x => x.Customer)
                .ThenInclude(x => x.User);
        }

        public async Task<PaginatedList<Rating>> GetArtisanRatingPage(PaginatedCommand paginated, string userId, 
            CancellationToken cancellationToken)
        {
            IQueryable<Rating> whereQueryable;
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
        
        public async Task<PaginatedList<Rating>> GetCustomerRatingPage(PaginatedCommand paginated, string userId, 
            CancellationToken cancellationToken)
        {
            IQueryable<Rating> whereQueryable;
            if (string.IsNullOrEmpty(paginated.OtherJson))
            {
                whereQueryable = base.GetBaseQuery().Where(x => x.Customer.UserId == userId)
                    .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));
            }
            else
            {
                whereQueryable = GetBaseQuery().Where(x => x.CustomerId == paginated.OtherJson)
                    .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));
            }

            var orders = await BuildPage(whereQueryable, paginated, cancellationToken);
            return orders;
        }


        private static async Task<PaginatedList<Rating>> BuildPage(IQueryable<Rating> whereQueryable, PaginatedCommand paginated,
            CancellationToken cancellationToken)
        {
            var pagedModel = await whereQueryable.PageBy(x => paginated.Take, paginated)
                .ToListAsync(cancellationToken);

            var totalRecords = await whereQueryable.CountAsync(cancellationToken: cancellationToken);

            return new PaginatedList<Rating>(data: pagedModel,
                totalCount: totalRecords,
                currentPage: paginated.PageNumber,
                pageSize: paginated.PageSize);
        }
    }
}