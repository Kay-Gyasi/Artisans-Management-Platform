using AMP.Domain.Entities;
using AMP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AMP.Persistence.Configurations
{
    public sealed class PaymentsConfiguration : DatabaseConfigurationBase<Payments>
    {
        public override void Configure(EntityTypeBuilder<Payments> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.OrderId)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(36);
            builder.Property(a => a.Reference)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(a => a.TransactionReference)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(a => a.AmountPaid)
                .HasDefaultValue(0.00);

        }
    }
}