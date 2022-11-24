namespace AMP.Persistence.Repositories
{
    [Repository]
    public class ServiceRepository : RepositoryBase<Services>, IServiceRepository
    {
        public ServiceRepository(AmpDbContext context, ILogger<Services> logger) : base(context, logger)
        {
        }

        public Task<List<Lookup>> GetAvailableServices()
        {
            return GetBaseQuery().Where(x => x.Artisans.Any())
                .Select(x => new Lookup()
                {
                    Id = x.Id,
                    Name = x.Name
                }).OrderBy(x => x.Name)
                    .ToListAsync();
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

        public async Task<string> GetNameAsync(string serviceId)
        {
            var service = await GetBaseQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == serviceId);
            return service?.Name;
        }
    }
}