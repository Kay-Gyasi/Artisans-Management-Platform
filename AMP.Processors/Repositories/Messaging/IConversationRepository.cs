using AMP.Domain.Entities.Messaging;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories.Messaging;

public interface IConversationRepository : IRepositoryBase<Conversation>
{
    Task<PaginatedList<Conversation>> GetConversationPage(PaginatedCommand paginated,
        string userId, CancellationToken cancellationToken);

    Task<Conversation> GetWithoutMessages(string id);
}