using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations;

public class RegistrationsConfiguration : DatabaseConfigurationBase<Registrations>
{
    public override void Configure(EntityTypeBuilder<Registrations> builder)
    {
        base.Configure(builder);
        builder.HasIndex(x => x.Phone)
            .HasDatabaseName("IX_Registration_Phone");
        builder.HasIndex(x => x.VerificationCode)
            .HasDatabaseName("IX_Code");
        builder.HasIndex(x => new{x.Phone, x.VerificationCode})
            .HasDatabaseName("IX_Code_Phone");
        builder.Property(x => x.Phone)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(15);
        builder.Property(x => x.VerificationCode)
            .IsRequired()
            .HasMaxLength(40);
    }
}