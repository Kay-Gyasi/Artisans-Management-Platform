using AMP.Domain.Entities;
using AMP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AMP.Persistence.Configurations
{
    public class DisputesConfiguration : DatabaseConfigurationBase<Disputes>
    {
        public override void Configure(EntityTypeBuilder<Disputes> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.Details)
                .IsRequired();
            builder.Property(a => a.Status)
                .HasDefaultValue(DisputeStatus.Open)
                .HasConversion(new EnumToStringConverter<DisputeStatus>());
        }
    }
}