using AMP.Domain.Entities.BusinessManagement;
using AMP.Processors.Repositories.BusinessManagement;

namespace AMP.Persistence.Repositories.BusinessManagement
{
    [Repository]
    public class ServiceRepository : RepositoryBase<Service>, IServiceRepository
    {
        public ServiceRepository(AmpDbContext context, ILogger<Service> logger) : base(context, logger)
        {
        }

        public Task<List<Lookup>> GetAvailableServices()
        {
            return GetBaseQuery()
                .Where(x => x.Artisans.Any())
                .Select(x => new Lookup()
                {
                    Id = x.Id,
                    Name = x.Name
                }).OrderBy(x => x.Name)
                .AsNoTracking()
                .ToListAsync();
        }

        public override Task<List<Lookup>> GetLookupAsync()
        {
            return GetBaseQuery()
                .Select(x => new Lookup()
            {
                Id = x.Id,
                Name = x.Name
            }).OrderBy(x => x.Name)
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<List<Service>> BuildServices(IEnumerable<string> services)
        {
            var results = new List<Service>();
            foreach (var service in services)
            {
                var build = await GetBaseQuery()
                    .FirstOrDefaultAsync(x => x.Name == service);
                results.Add(build);
            }
            return results;
        }

        public async Task<string> GetNameAsync(string serviceId)
        {
            var service = await GetBaseQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == serviceId);
            return service?.Name;
        }
    }
}