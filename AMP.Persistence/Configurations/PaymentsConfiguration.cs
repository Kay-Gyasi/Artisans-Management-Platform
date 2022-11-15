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
            builder.HasIndex(x => x.Reference)
                .HasDatabaseName("IX_Reference");
            builder.HasIndex(x => x.TransactionReference)
                .HasDatabaseName("IX_TrxRef");
            builder.HasIndex(x => new{x.OrderId, x.IsVerified})
                .HasDatabaseName("IX_Order_Verified");
            builder.HasIndex(x => new{x.IsVerified})
                .HasDatabaseName("IX_Artisan_Verified");
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