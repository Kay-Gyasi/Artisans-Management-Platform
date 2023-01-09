using AMP.Domain.Entities.Messaging;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations.Messaging;

public class ConversationsConfiguration : DatabaseConfigurationBase<Conversation>
{
    public override void Configure(EntityTypeBuilder<Conversation> builder)
    {
        base.Configure(builder);
        builder.ToTable("Conversations");
        builder.Ignore(x => x.UnreadMessages);
        builder.Property(a => a.FirstParticipantId)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(36);
        builder.Property(a => a.SecondParticipantId)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(36);
        builder.HasOne(x => x.FirstParticipant)
            .WithMany(x => x.FirstParticipantConvos)
            .HasForeignKey(x => x.FirstParticipantId);
        builder.HasOne(x => x.SecondParticipant)
            .WithMany(x => x.SecondParticipantConvos)
            .HasForeignKey(x => x.SecondParticipantId);
    }
}