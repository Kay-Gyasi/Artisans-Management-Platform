using AMP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations
{
    public class RatingsConfiguration : DatabaseConfigurationBase<Ratings>
    {
        public override void Configure(EntityTypeBuilder<Ratings> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.CustomerId)
                .IsRequired();
            builder.Property(a => a.ArtisanId)
                .IsRequired();
            builder.Property(a => a.Votes)
                .IsRequired()
                .HasDefaultValue(0);
        }
    }
}