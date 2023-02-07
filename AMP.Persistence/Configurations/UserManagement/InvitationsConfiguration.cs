using AMP.Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations.UserManagement;

public class InvitationsConfiguration : DatabaseConfiguration<Invitation>
{
    public override void Configure(EntityTypeBuilder<Invitation> builder)
    {
        base.Configure(builder);
        builder.ToTable("Invitations");
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