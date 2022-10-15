using AMP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations
{
    public sealed class RequestsConfiguration : DatabaseConfigurationBase<Requests>
    {
        public override void Configure(EntityTypeBuilder<Requests> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.ArtisanId)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(36);
            builder.Property(x => x.OrderId)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(36);
            builder.Property(x => x.CustomerId)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(36);
        }
    }
}