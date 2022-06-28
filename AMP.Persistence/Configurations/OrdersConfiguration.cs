using AMP.Domain.Entities;
using AMP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AMP.Persistence.Configurations
{
    public class OrdersConfiguration : DatabaseConfigurationBase<Orders>
    {
        public override void Configure(EntityTypeBuilder<Orders> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.CustomerId)
                .IsRequired();
            builder.Property(a => a.ServiceId)
                .IsRequired();
            builder.Property(a => a.Description)
                .IsRequired();
            builder.Property(a => a.Urgency)
                .HasDefaultValue(Urgency.Medium)
                .HasConversion(new EnumToStringConverter<Urgency>());
            builder.Property(a => a.Status)
                .HasDefaultValue(OrderStatus.Placed)
                .HasConversion(new EnumToStringConverter<OrderStatus>());
            builder.OwnsOne(x => x.WorkAddress, a =>
            {
                a.Property(x => x.StreetAddress).IsRequired();
                a.Property(x => x.Country).HasDefaultValue("Ghana");
            });
            builder.HasOne(a => a.Payment)
                .WithOne(a => a.Order)
                .HasForeignKey<Payments>(c => c.OrderId);
        }
    }
}