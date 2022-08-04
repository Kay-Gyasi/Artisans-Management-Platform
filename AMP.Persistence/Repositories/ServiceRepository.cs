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
    public class ServiceRepository : RepositoryBase<Services>, IServiceRepository
    {
        public ServiceRepository(AmpDbContext context, ILogger<Services> logger) : base(context, logger)
        {
        }

        public override Task<List<Lookup>> GetLookupAsync()
        {
            return GetBaseQuery().Select(x => new Lookup()
                {
                    Id = x.Id,
                    Name = x.Name
                }).OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<List<Services>> BuildServices(List<string> services)
        {
            var results = new List<Services>();
            foreach (var service in services)
            {
                var build = await GetBaseQuery().FirstOrDefaultAsync(x => x.Name == service);
                results.Add(build);
            }
            return results;
        }
    }
}