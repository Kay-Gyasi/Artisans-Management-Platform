using AMP.Domain.Entities;
using AMP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AMP.Persistence.Configurations
{
    public sealed class DisputesConfiguration : DatabaseConfigurationBase<Disputes>
    {
        public override void Configure(EntityTypeBuilder<Disputes> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.Details)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(250);
            builder.Property(a => a.CustomerId)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(36);
            builder.Property(a => a.OrderId)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(36);
            builder.Property(a => a.Status)
                .HasDefaultValue(DisputeStatus.Open)
                .HasConversion(new EnumToStringConverter<DisputeStatus>());
        }
    }
}