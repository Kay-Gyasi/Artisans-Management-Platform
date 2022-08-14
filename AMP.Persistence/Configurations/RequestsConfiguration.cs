using AMP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations
{
    public class RequestsConfiguration : DatabaseConfigurationBase<Requests>
    {
        public override void Configure(EntityTypeBuilder<Requests> builder)
        {
            builder.Property(x => x.ArtisanId).IsRequired();
            builder.Property(x => x.OrderId).IsRequired();
            builder.Property(x => x.CustomerId).IsRequired();
            base.Configure(builder);
        }
    }
}