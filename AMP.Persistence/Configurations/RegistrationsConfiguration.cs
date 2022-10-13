using AMP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations;

public class RegistrationsConfiguration : DatabaseConfigurationBase<Registrations>
{
    public override void Configure(EntityTypeBuilder<Registrations> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Phone)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(15);
        builder.Property(x => x.VerificationCode)
            .IsRequired()
            .HasMaxLength(40);
    }
}