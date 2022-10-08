using System.Linq;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Domain.Enums;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    [Repository]
    public class DisputeRepository : RepositoryBase<Disputes>, IDisputeRepository
    {
        public DisputeRepository(AmpDbContext context, ILogger<Disputes> logger) : base(context, logger)
        {
        }

        public async Task<int> OpenDisputeCount(string userId) =>
            await GetBaseQuery().Where(x => x.Customer.UserId == userId 
                                            && x.Status == DisputeStatus.Open).CountAsync();

        public override IQueryable<Disputes> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.Order)
                .Include(x => x.Customer);
        }


    }
}