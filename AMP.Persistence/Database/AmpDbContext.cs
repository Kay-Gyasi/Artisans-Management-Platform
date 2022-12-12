using AMP.Domain.Entities.Base;
using AMP.Persistence.Configurations;
using Languages = AMP.Domain.Entities.Languages;

namespace AMP.Persistence.Database
{
    public class AmpDbContext : DbContext
    {
        public AmpDbContext() { }

        public AmpDbContext(DbContextOptions<AmpDbContext> options)
            : base(options) { }

        public DbSet<Artisans> Artisans { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Disputes> Disputes { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Requests> Requests { get; set; }
        public DbSet<Registrations> Registrations { get; set; }
        public DbSet<Invitations> Invitations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=YOGA-X1;Integrated Security=True;Initial Catalog=AmpDevDb;");
            }

            //optionsBuilder.LogTo(Console.WriteLine, new[] {DbLoggerCategory.Query.Name});
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ArtisansConfiguration).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entity in this.ChangeTracker.Entries()
                         .Where(e => e.Entity is EntityBase 
                                     && e.State is EntityState.Added or EntityState.Modified)
                         .Select(e => e.Entity as EntityBase)
                    )
            {
                if (entity != null)
                {
                    entity.DateModified = DateTime.Now;
                }
            }
            
            foreach (var entity in this.ChangeTracker.Entries()
                         .Where(e => e.Entity is EntityBase 
                                     && e.State is EntityState.Added)
                         .Select(e => e.Entity as EntityBase)
                    )
            {
                if (entity != null)
                {
                    entity.DateCreated = DateTime.Now;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}