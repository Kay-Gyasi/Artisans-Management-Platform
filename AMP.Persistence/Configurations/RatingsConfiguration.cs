using AMP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations
{
    public sealed class RatingsConfiguration : DatabaseConfigurationBase<Ratings>
    {
        public override void Configure(EntityTypeBuilder<Ratings> builder)
        {
            base.Configure(builder);
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