using AMP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations
{
    public class ArtisansConfiguration : DatabaseConfigurationBase<Artisans>
    {
        public override void Configure(EntityTypeBuilder<Artisans> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.UserId).IsRequired();
            builder.Property(a => a.BusinessName).IsRequired();
            builder.Property(a => a.Description).IsRequired();
            builder.Property(a => a.IsVerified).HasDefaultValue(false);
            builder.Property(a => a.IsApproved).HasDefaultValue(false);
        }
    }
}