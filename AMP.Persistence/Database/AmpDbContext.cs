using AMP.Domain.Entities.Base;
using AMP.Domain.Entities.BusinessManagement;
using AMP.Domain.Entities.Messaging;
using AMP.Domain.Entities.UserManagement;
using AMP.Persistence.Configurations;
using AMP.Persistence.Configurations.UserManagement;

namespace AMP.Persistence.Database
{
    public class AmpDbContext : DbContext
    {
        public AmpDbContext() { }

        public AmpDbContext(DbContextOptions<AmpDbContext> options)
            : base(options) { }

        public DbSet<Artisan> Artisans { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dispute> Disputes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConnectRequest> ConnectRequests { get; set; }
        public DbSet<Notification> Notifications { get; set; }

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
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            var modified = ChangeTracker.Entries()
                .Where(e => e.Entity is EntityBase
                            && e.State is EntityState.Added or EntityState.Modified)
                .Select(e => e.Entity as EntityBase);
            foreach (var entity in modified)
            {
                if (entity == null) continue;
                if(entity.IgnoreDateModified) continue;
                entity.DateModified = DateTime.Now;
            }

            var added = ChangeTracker.Entries()
                .Where(e => e.Entity is EntityBase
                            && e.State is EntityState.Added)
                .Select(e => e.Entity as EntityBase);
            foreach (var entity in added)
            {
                if (entity == null) continue;
                if(entity.IgnoreDateModified) continue;
                entity.DateCreated = DateTime.Now;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}