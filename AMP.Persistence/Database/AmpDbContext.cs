using System;
using Microsoft.EntityFrameworkCore;

namespace AMP.Persistence.Database
{
    public class AmpDbContext : DbContext
    {
        public AmpDbContext()
        {
        }

        public AmpDbContext(DbContextOptions<AmpDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // connection string here
            }
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
            base.OnModelCreating(modelBuilder);
        }
    }
}