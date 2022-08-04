using System.Threading.Tasks;
using AMP.Persistence.Database;
using AMP.Processors.Repositories.Administration;
using Microsoft.EntityFrameworkCore;

namespace AMP.Persistence.Repositories.Administration
{
    [Repository]
    public class InitializeDbRepository : IInitializeDbRepository
    {
        private readonly AmpDbContext _context;

        public InitializeDbRepository(AmpDbContext context)
        {
            _context = context;
        }

        public async Task InitializeDatabase()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.MigrateAsync();
        }
    }
}