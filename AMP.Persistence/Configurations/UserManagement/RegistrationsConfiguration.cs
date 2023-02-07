using AMP.Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations.UserManagement;

public class RegistrationsConfiguration : DatabaseConfiguration<Registration>
{
    public override void Configure(EntityTypeBuilder<Registration> builder)
    {
        base.Configure(builder);
        builder.ToTable("Registrations");
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