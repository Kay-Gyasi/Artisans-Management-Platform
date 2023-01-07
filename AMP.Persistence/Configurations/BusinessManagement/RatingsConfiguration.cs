using AMP.Domain.Entities.BusinessManagement;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations.BusinessManagement
{
    public sealed class RatingsConfiguration : DatabaseConfigurationBase<Rating>
    {
        public override void Configure(EntityTypeBuilder<Rating> builder)
        {
            base.Configure(builder);
            builder.ToTable("Ratings");
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