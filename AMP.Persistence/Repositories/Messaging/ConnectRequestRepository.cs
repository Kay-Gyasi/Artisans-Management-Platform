using AMP.Domain.Entities.Messaging;
using AMP.Processors.Repositories.Messaging;

namespace AMP.Persistence.Repositories.Messaging;

[Repository]
public class ConnectRequestRepository : RepositoryBase<ConnectRequest>, IConnectRequestRepository
{
    public ConnectRequestRepository(AmpDbContext context, ILogger<ConnectRequest> logger) : base(context, logger)
    {
    }
    
    public async Task<PaginatedList<ConnectRequest>> GetChatInvitesPage(PaginatedCommand paginated,
        string userId, CancellationToken cancellationToken)
    {
        var whereQueryable = GetBaseQuery().Where(x => x.InviteeId == userId)
            .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));
        var orders = await whereQueryable.BuildPage(paginated, cancellationToken);
        return orders;
    }
}