using AMP.Domain.Entities;
using AMP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AMP.Persistence.Configurations
{
    public sealed class OrdersConfiguration : DatabaseConfigurationBase<Orders>
    {
        public override void Configure(EntityTypeBuilder<Orders> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.CustomerId)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(36);
            builder.Property(a => a.ArtisanId)
                .HasColumnType("varchar")
                .HasMaxLength(36);
            builder.Property(a => a.ServiceId)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(36);
            builder.Property(a => a.Description)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(200);
            builder.Property(a => a.IsComplete)
                .HasDefaultValue(false);
            builder.Property(a => a.Urgency)
                .HasDefaultValue(Urgency.Medium)
                .HasConversion(new EnumToStringConverter<Urgency>());
            builder.Property(a => a.Status)
                .HasDefaultValue(OrderStatus.Placed)
                .HasConversion(new EnumToStringConverter<OrderStatus>());
            builder.Property(a => a.Scope)
                .HasDefaultValue(ScopeOfWork.Maintenance)
                .HasConversion(new EnumToStringConverter<ScopeOfWork>());
            builder.OwnsOne(x => x.WorkAddress, a =>
            {
                a.Property(x => x.StreetAddress)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(100);
                a.Property(x => x.Country)
                    .HasDefaultValue(Countries.Ghana)
                    .HasConversion(new EnumToStringConverter<Countries>());
            });
        }
    }
}