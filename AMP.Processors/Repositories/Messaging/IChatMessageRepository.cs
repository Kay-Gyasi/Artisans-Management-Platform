using AMP.Domain.Entities.Messaging;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories.Messaging;

public interface IChatMessageRepository : IRepository<ChatMessage>
{
    Task<int> GetUnreadMessageCount(string userId);
}