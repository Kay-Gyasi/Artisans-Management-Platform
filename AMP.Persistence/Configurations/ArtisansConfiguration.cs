using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AMP.Persistence.Configurations
{
    public sealed class ArtisansConfiguration : DatabaseConfigurationBase<Artisans>
    {
        public override void Configure(EntityTypeBuilder<Artisans> builder)
        {
            base.Configure(builder);
            builder.HasIndex(x => x.BusinessName)
                .HasDatabaseName("Index_Artisan_BusinessName");
            builder.HasIndex(x => x.Type)
                .HasDatabaseName("Index_Artisan_Type");
            builder.Property(a => a.UserId)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(36);
            builder.Property(a => a.BusinessName)
                .IsRequired().HasColumnType("varchar")
                .HasMaxLength(70);
            builder.Property(a => a.Description)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(500);
            builder.Property(a => a.IsVerified).HasDefaultValue(false);
            builder.Property(a => a.IsApproved).HasDefaultValue(false);
            builder.Property(a => a.Type)
                .HasDefaultValue(BusinessType.Individual)
                .HasConversion(new EnumToStringConverter<BusinessType>())
                .HasColumnType("varchar")
                .HasMaxLength(12);
            builder.Property(x => x.IsVerified)
                .HasDefaultValue(false);
        }
    }
}