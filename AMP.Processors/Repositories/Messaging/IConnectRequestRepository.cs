using AMP.Domain.Entities.Messaging;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories.Messaging;

public interface IConnectRequestRepository : IRepository<ConnectRequest>
{
    Task<PaginatedList<ConnectRequest>> GetChatInvitesPage(PaginatedCommand paginated,
        string userId, CancellationToken cancellationToken);

    Task<bool> IsRequested(string id1, string id2);
    Task<int> GetConnectRequestsCount(string userId);
}