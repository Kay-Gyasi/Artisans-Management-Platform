using AMP.Domain.Entities.Messaging;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories.Messaging;

public interface IChatMessageRepository : IRepositoryBase<ChatMessage>
{
    Task<int> GetUnreadMessageCount(string userId);
}