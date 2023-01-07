using System.Data;
using AMP.Domain.Entities.UserManagement;
using AMP.Processors.Repositories.UserManagement;

namespace AMP.Persistence.Repositories.UserManagement
{
    [Repository]
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        private readonly IDapperContext _dapperContext;

        public CustomerRepository(AmpDbContext context, ILogger<Customer> logger,
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

        public override IQueryable<Customer> GetBaseQuery()
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


        public async Task<Customer> GetByUserIdAsync(string userId) 
            => await GetBaseQuery().FirstOrDefaultAsync(x => x.UserId == userId);

        public async Task<string> GetCustomerId(string userId) 
            => (await GetByUserIdAsync(userId)).Id;
    }
}