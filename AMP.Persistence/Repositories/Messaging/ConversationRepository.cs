using AMP.Domain.Entities.Messaging;
using AMP.Processors.Repositories.Messaging;

namespace AMP.Persistence.Repositories.Messaging;

[Repository]
public class ConversationRepository : RepositoryBase<Conversation>, IConversationRepository
{
    public ConversationRepository(AmpDbContext context, ILogger<Conversation> logger) : base(context, logger)
    {
    }
    
    public async Task<PaginatedList<Conversation>> GetConversationPage(PaginatedCommand paginated,
        string userId, CancellationToken cancellationToken)
    {
        var whereQueryable = GetBaseQuery().Where(x => x.FirstParticipantId == userId
            || x.SecondParticipantId == userId)
            .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));
        return await whereQueryable
            .OrderByDescending(x => x.DateModified)
            .BuildPage(paginated, cancellationToken);
    }
    
    public async Task<bool> IsConnected(string firstId, string secondId, string conversationId)
    {
        return await base.GetBaseQuery()
            .AnyAsync(x => x.Id == conversationId 
                           && (x.FirstParticipantId == firstId || x.FirstParticipantId == secondId)
                           && (x.SecondParticipantId == firstId || x.SecondParticipantId == secondId));
    }

    public async Task<Conversation> GetWithoutMessages(string id)
    {
        return await Context.Conversations.FirstOrDefaultAsync(x => x.Id == id);
    }

    public override IQueryable<Conversation> GetBaseQuery()
    {
        return base.GetBaseQuery()
            .Include(x => x.Messages)
            .Include(x => x.FirstParticipant)
            .ThenInclude(x => x.Image)
            .Include(x => x.SecondParticipant)
            .ThenInclude(x => x.Image);
    }
}