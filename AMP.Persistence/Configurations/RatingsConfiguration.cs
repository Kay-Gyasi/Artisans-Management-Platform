using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations
{
    public sealed class RatingsConfiguration : DatabaseConfigurationBase<Ratings>
    {
        public override void Configure(EntityTypeBuilder<Ratings> builder)
        {
            base.Configure(builder);
            builder.HasIndex(x => new {x.ArtisanId, x.CustomerId})
                .HasDatabaseName("IX_ArtisanId_CustomerId");
            builder.HasIndex(x => x.ArtisanId)
                .HasDatabaseName("IX_ArtisanId");
            builder.HasIndex(x => x.CustomerId)
                .HasDatabaseName("IX_CustomerId");
            builder.Property(a => a.CustomerId)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(36);
            builder.Property(a => a.ArtisanId)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(36);
            builder.Property(a => a.Description)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(150);
            builder.Property(a => a.Votes)
                .IsRequired()
                .HasDefaultValue(0);
        }
    }
}