using AMP.Domain.Entities.BusinessManagement;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AMP.Persistence.Configurations.BusinessManagement
{
    public sealed class OrdersConfiguration : DatabaseConfigurationBase<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
            builder.ToTable("Orders");
            builder.HasIndex(x => x.ReferenceNo)
                .HasDatabaseName("IX_Orders_ReferenceNo");
            builder.HasIndex(x => new {x.Id, x.IsArtisanComplete})
                .HasDatabaseName("IX_Id_IsArtisanComplete");
            builder.HasIndex(x => new {x.Status, x.IsRequestAccepted})
                .HasDatabaseName("IX_Status_RequestAc_UserId");
            builder.HasIndex(x => new {x.Status})
                .HasDatabaseName("IX_Status_ArtisanUserId");
            builder.HasIndex(x => new{x.ServiceId})
                .HasDatabaseName("IX_Service_CusUserId");
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