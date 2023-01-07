using AMP.Domain.Entities.BusinessManagement;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations.BusinessManagement
{
    public sealed class RequestsConfiguration : DatabaseConfigurationBase<Request>
    {
        public override void Configure(EntityTypeBuilder<Request> builder)
        {
            base.Configure(builder);
            builder.ToTable("Requests");
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