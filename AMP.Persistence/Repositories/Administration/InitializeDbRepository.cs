using AMP.Persistence.Database;
using AMP.Processors.Repositories.Administration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

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
            try
            {
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Artisans");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Customers");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Disputes");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Images");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Languages");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE LanguagesUsers");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Orders");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Payments");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Ratings");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Requests");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Services");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Users");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //throw;
            }
            await _context.Database.MigrateAsync();
        }
    }
}