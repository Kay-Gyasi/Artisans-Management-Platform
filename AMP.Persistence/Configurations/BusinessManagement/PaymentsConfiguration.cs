using AMP.Domain.Entities.BusinessManagement;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations.BusinessManagement
{
    public sealed class PaymentsConfiguration : DatabaseConfiguration<Payment>
    {
        public override void Configure(EntityTypeBuilder<Payment> builder)
        {
            base.Configure(builder);
            builder.ToTable("Payments");
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