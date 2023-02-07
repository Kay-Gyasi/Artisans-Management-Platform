using AMP.Domain.Entities.UserManagement;
using AMP.Processors.Repositories.UserManagement;

namespace AMP.Persistence.Repositories.UserManagement
{
    [Repository]
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        public LanguageRepository(AmpDbContext context, ILogger<Language> logger) : base(context, logger)
        {
        }

        public async Task<List<Language>> BuildLanguages(IEnumerable<string> languages)
        {
            var results = new List<Language>();
            foreach (var language in languages)
            {
                var build = await GetBaseQuery().FirstOrDefaultAsync(x => x.Name == language);
                results.Add(build);
            }
            return results;
        }
    }
}