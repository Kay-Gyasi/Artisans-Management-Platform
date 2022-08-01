using System;
using AMP.Domain.Entities;
using AMP.Domain.Enums;
using AMP.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Languages = AMP.Domain.Entities.Languages;

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

        public DbSet<Artisans> Artisans { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Disputes> Disputes { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Languages> Languages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost; Database=AmpDevDb; Username=postgres; Password=postgres;Include Error Detail=true");
            }
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ArtisansConfiguration).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}