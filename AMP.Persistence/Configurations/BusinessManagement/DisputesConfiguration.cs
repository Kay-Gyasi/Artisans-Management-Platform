using AMP.Domain.Entities.BusinessManagement;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AMP.Persistence.Configurations.BusinessManagement
{
    public sealed class DisputesConfiguration : DatabaseConfiguration<Dispute>
    {
        public override void Configure(EntityTypeBuilder<Dispute> builder)
        {
            base.Configure(builder);
            builder.ToTable("Disputes");
            builder.HasIndex(x => x.Status)
                .HasDatabaseName("IX_Status");
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