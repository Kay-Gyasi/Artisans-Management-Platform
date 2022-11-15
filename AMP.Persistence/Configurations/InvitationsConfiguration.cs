using AMP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations;

public class InvitationsConfiguration : DatabaseConfigurationBase<Invitations>
{
    public override void Configure(EntityTypeBuilder<Invitations> builder)
    {
        base.Configure(builder);
        builder.HasIndex(x => x.InvitedPhone)
            .HasDatabaseName("Index_Phone");
        builder.Property(x => x.InvitedPhone)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(15);
        builder.Property(x => x.InvitedPhone)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(36);
        builder.Property(x => x.Type)
            .IsRequired();
    }
}