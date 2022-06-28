using AMP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations
{
    public class CustomersConfiguration : DatabaseConfigurationBase<Customers>
    {
        public override void Configure(EntityTypeBuilder<Customers> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.UserId).IsRequired();
        }
    }
}