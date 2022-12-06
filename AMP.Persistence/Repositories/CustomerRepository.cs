using System.Data;
using AMP.Processors.Exceptions;
using AMP.Processors.Repositories.Base;

namespace AMP.Persistence.Repositories
{
    [Repository]
    public class CustomerRepository : RepositoryBase<Customers>, ICustomerRepository
    {
        private readonly IDapperContext _dapperContext;

        public CustomerRepository(AmpDbContext context, ILogger<Customers> logger,
            IDapperContext dapperContext) : base(context, logger)
        {
            _dapperContext = dapperContext;
        }
        
        public async Task DeleteAsync(string id)
        {
            var rows = await _dapperContext.Execute(
                "UPDATE Customers SET EntityStatus = 'Deleted', DateModified = GETDATE() " +
                $"WHERE Id = '{id}'",
                null,
                CommandType.Text);
            if (rows == 0) throw new InvalidIdException($"Customer with id: {id} does not exist");
        }

        public override IQueryable<Customers> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.User)
                .ThenInclude(x => x.Languages)
                .Include(x => x.User)
                .ThenInclude(x => x.Image);
        }

        public override Task<List<Lookup>> GetLookupAsync()
        {
            return GetBaseQuery().Select(x => new Lookup()
                {
                    Id = x.Id,
                    Name = x.User.DisplayName
                }).OrderBy(x => x.Name)
                .ToListAsync();
        }


        public async Task<Customers> GetByUserIdAsync(string userId) 
            => await GetBaseQuery().FirstOrDefaultAsync(x => x.UserId == userId);

        public async Task<string> GetCustomerId(string userId) 
            => (await GetByUserIdAsync(userId)).Id;
    }
}