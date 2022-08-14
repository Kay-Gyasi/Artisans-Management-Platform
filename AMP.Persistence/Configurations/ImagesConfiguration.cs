using AMP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations
{
    public class ImagesConfiguration : DatabaseConfigurationBase<Images>
    {
        public override void Configure(EntityTypeBuilder<Images> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.PublicId)
                .IsRequired();
            builder.Property(a => a.ImageUrl)
                .IsRequired();
        }
    }
}