using AMP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations
{
    public class SchedulesConfiguration : DatabaseConfigurationBase<Schedules>
    {
        public override void Configure(EntityTypeBuilder<Schedules> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.ArtisanId)
                .IsRequired();
            builder.Property(a => a.Date)
                .IsRequired();
            builder.Property(a => a.Time)
                .IsRequired();
        }
    }
}