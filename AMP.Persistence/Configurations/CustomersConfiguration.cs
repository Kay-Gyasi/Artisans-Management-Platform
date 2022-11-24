using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations
{
    public sealed class CustomersConfiguration : DatabaseConfigurationBase<Customers>
    {
        public override void Configure(EntityTypeBuilder<Customers> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.UserId)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(36);
        }
    }
}