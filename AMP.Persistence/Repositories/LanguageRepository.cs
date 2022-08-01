using System.Collections.Generic;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    [Repository]
    public class LanguageRepository : RepositoryBase<Languages>, ILanguageRepository
    {
        public LanguageRepository(AmpDbContext context, ILogger<Languages> logger) : base(context, logger)
        {
        }

        public async Task<List<Languages>> BuildLanguages(List<string> languages)
        {
            var results = new List<Languages>();
            foreach (var language in languages)
            {
                var build = await GetBaseQuery().FirstOrDefaultAsync(x => x.Name == language);
                results.Add(build);
            }
            return results;
        }
    }
}