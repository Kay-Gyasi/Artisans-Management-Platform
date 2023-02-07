using AMP.Domain.Entities.Messaging;
using AMP.Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations.Messaging;

public class ChatMessagesConfiguration : DatabaseConfiguration<ChatMessage>
{
    public override void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        base.Configure(builder);
        builder.ToTable("ChatMessages");
        builder.Property(a => a.ConversationId)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(36);
        builder.Property(a => a.ReceiverId)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(36);
        builder.Property(a => a.ConversationId)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(36);
        builder.Property(x => x.Message)
            .HasMaxLength(200);
        builder.HasOne(x => x.Receiver)
            .WithMany(x => x.ReceivedMessages)
            .HasForeignKey(x => x.ReceiverId);
        builder.HasOne(x => x.Sender)
            .WithMany(x => x.SentMessages)
            .HasForeignKey(x => x.SenderId);
    }
}