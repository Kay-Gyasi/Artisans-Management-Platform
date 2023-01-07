using AMP.Domain.Entities.Messaging;
using AMP.Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations.Messaging;

public class ConnectRequestsConfiguration : DatabaseConfigurationBase<ConnectRequest>
{
    public override void Configure(EntityTypeBuilder<ConnectRequest> builder)
    {
        base.Configure(builder);
        builder.ToTable("ConnectRequests");
        builder.Property(x => x.InviterId)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(36);
        builder.Property(x => x.InviteeId)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(36);
        builder.HasOne(x => x.Invitee)
            .WithMany(x => x.InviteeConnectRequests)
            .HasForeignKey(x => x.InviteeId);
        builder.HasOne(x => x.Inviter)
            .WithMany(x => x.InviterConnectRequests)
            .HasForeignKey(x => x.InviterId);
    }
}