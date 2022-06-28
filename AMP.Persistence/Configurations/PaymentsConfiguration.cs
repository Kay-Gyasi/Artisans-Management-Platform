using AMP.Domain.Entities;
using AMP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AMP.Persistence.Configurations
{
    public class PaymentsConfiguration : DatabaseConfigurationBase<Payments>
    {
        public override void Configure(EntityTypeBuilder<Payments> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.CustomerId)
                .IsRequired();
            builder.Property(a => a.OrderId)
                .IsRequired();
            builder.Property(a => a.Status)
                .HasDefaultValue(PaymentStatus.NotSent) // TODO:: Check this
                .HasConversion(new EnumToStringConverter<PaymentStatus>());
            builder.Property(a => a.AmountPaid)
                .HasDefaultValue(0.00);

        }
    }
}