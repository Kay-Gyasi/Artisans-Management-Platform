using AMP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations
{
    public class ServicesConfiguration : DatabaseConfigurationBase<Services>
    {
        public override void Configure(EntityTypeBuilder<Services> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.Name)
                .IsRequired();
        }
    }
}