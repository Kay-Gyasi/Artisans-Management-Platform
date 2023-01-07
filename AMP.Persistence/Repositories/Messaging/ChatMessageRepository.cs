using AMP.Domain.Entities.Messaging;
using AMP.Processors.Repositories.Messaging;

namespace AMP.Persistence.Repositories.Messaging;

[Repository]
public class ChatMessageRepository : RepositoryBase<ChatMessage>, IChatMessageRepository
{
    public ChatMessageRepository(AmpDbContext context, ILogger<ChatMessage> logger) : base(context, logger)
    {
    }

    public async Task<bool> IsConnected(string firstId, string secondId)
    {
        return await base.GetBaseQuery()
            .AnyAsync(x => (x.Conversation.FirstParticipantId == firstId &&
                           x.Conversation.SecondParticipantId == secondId) ||
                      (x.Conversation.FirstParticipantId == secondId &&
                      x.Conversation.SecondParticipantId == firstId));
    }
}