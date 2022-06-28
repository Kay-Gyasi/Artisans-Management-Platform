using AMP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations
{
    public class ProposalsConfiguration : DatabaseConfigurationBase<Proposals>
    {
        public override void Configure(EntityTypeBuilder<Proposals> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.OrderId)
                .IsRequired();
            builder.Property(a => a.ArtisanId)
                .IsRequired();
            builder.Property(a => a.IsAccepted)
                .HasDefaultValue(false);
        }
    }
}