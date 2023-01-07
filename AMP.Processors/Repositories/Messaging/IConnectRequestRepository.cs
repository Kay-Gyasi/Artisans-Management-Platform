using AMP.Domain.Entities.Messaging;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories.Messaging;

public interface IConnectRequestRepository : IRepositoryBase<ConnectRequest>
{
    Task<PaginatedList<ConnectRequest>> GetChatInvitesPage(PaginatedCommand paginated,
        string userId, CancellationToken cancellationToken);
}