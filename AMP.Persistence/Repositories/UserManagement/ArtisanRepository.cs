using System.Data;
using AMP.Domain.Entities.UserManagement;
using AMP.Processors.Repositories.UserManagement;

namespace AMP.Persistence.Repositories.UserManagement
{
    [Repository]
    public class ArtisanRepository : RepositoryBase<Artisan>, IArtisanRepository
    {
        private readonly IDapperContext _dapperContext;

        public ArtisanRepository(AmpDbContext context, ILogger<Artisan> logger,
            IDapperContext dapperContext) : base(context, logger)
        {
            _dapperContext = dapperContext;
        }
        
        public async Task SoftDeleteAsync(string id)
        {
            var rows = await _dapperContext.Execute(
                "UPDATE Artisans SET EntityStatus = 'Deleted', DateModified = GETDATE() " +
                $"WHERE Id = '{id}'",
                null,
                CommandType.Text);
            if (rows == 0) throw new InvalidIdException($"Artisan with id: {id} does not exist");
        }

        public async Task<List<Lookup>> GetArtisansWhoHaveWorkedForCustomer(string userId)
        {
            var artisans = GetBaseQuery()
                .Where(x => x.Orders.Any(a => a.Customer.UserId == userId));
            return await artisans.Select(x => new Lookup
            {
                Id = x.Id,
                Name = x.BusinessName
            }).ToListAsync();
        }

        public async Task<Artisan> GetArtisanByUserId(string userId) 
            => await GetBaseQuery().FirstOrDefaultAsync(x => x.UserId == userId);

        public async Task<PaginatedList<Artisan>> GetArtisanPage(PaginatedCommand paginated, CancellationToken cancellationToken)
        {
            var whereQueryable = GetBaseQuery()
                .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search))
                .WhereIf(!string.IsNullOrEmpty(paginated.OtherJson), 
                    x => x.User.DisplayName.Contains(paginated.OtherJson) 
                         || x.BusinessName.Contains(paginated.OtherJson))
                .OrderByDescending(x => x.Ratings.Sum(a => a.Votes));
            
            return await whereQueryable.BuildPage(paginated, cancellationToken);
        }

        protected override Expression<Func<Artisan, bool>> GetSearchCondition(string search) 
            => x => x.Services.Any(a => a.Name == search);


        public override Task<List<Lookup>> GetLookupAsync()
        {
            return GetBaseQuery().Select(x => new Lookup()
            {
                Id = x.Id,
                Name = x.BusinessName
            }).OrderBy(x => x.Name)
                .ToListAsync();
        }

        public override IQueryable<Artisan> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.User)
                .ThenInclude(x => x.Languages)
                .Include(x => x.Services)
                .Include(x => x.Ratings)
                .Include(x => x.User)
                .ThenInclude(x => x.Image); ;
        }
    }
}