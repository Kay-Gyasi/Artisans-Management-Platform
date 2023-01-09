﻿using AMP.Domain.Entities.Messaging;
using AMP.Processors.Repositories.Messaging;

namespace AMP.Persistence.Repositories.Messaging;

[Repository]
public class ChatMessageRepository : RepositoryBase<ChatMessage>, IChatMessageRepository
{
    public ChatMessageRepository(AmpDbContext context, ILogger<ChatMessage> logger) : base(context, logger)
    {
    }

    public override IQueryable<ChatMessage> GetBaseQuery()
    {
        return base.GetBaseQuery()
            .Include(x => x.Sender)
            .ThenInclude(x => x.Image)
            .Include(x => x.Receiver)
            .ThenInclude(x => x.Image);
    }
}