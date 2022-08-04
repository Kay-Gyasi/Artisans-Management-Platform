using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Domain.ViewModels;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    [Repository]
    public class CustomerRepository : RepositoryBase<Customers>, ICustomerRepository
    {
        public CustomerRepository(AmpDbContext context, ILogger<Customers> logger) : base(context, logger)
        {
        }

        public override IQueryable<Customers> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.User)
                .ThenInclude(x => x.Languages);
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


        public async Task<Customers> GetByUserIdAsync(int userId)
        {
            return await GetBaseQuery().FirstOrDefaultAsync(x => x.UserId == userId);
        }
        
        public async Task<int> GetCustomerId(int userId)
        {
            var customer = await GetByUserIdAsync(userId);
            return customer.Id;
        }
    }
}