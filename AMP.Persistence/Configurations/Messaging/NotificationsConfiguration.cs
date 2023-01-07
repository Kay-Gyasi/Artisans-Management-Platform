using AMP.Domain.Entities.Messaging;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations.Messaging;

public class NotificationsConfiguration : DatabaseConfigurationBase<Notification>
{
    public override void Configure(EntityTypeBuilder<Notification> builder)
    {
        base.Configure(builder);
        builder.ToTable("Notifications");
        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(36);
    }
}