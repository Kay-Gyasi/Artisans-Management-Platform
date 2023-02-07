using AMP.Domain.Entities.Messaging;
using AMP.Processors.Repositories.Messaging;

namespace AMP.Persistence.Repositories.Messaging;

[Repository]
public class ConnectRequestRepository : Repository<ConnectRequest>, IConnectRequestRepository
{
    public ConnectRequestRepository(AmpDbContext context, ILogger<ConnectRequest> logger) : base(context, logger)
    {
    }
    
    public async Task<PaginatedList<ConnectRequest>> GetChatInvitesPage(PaginatedCommand paginated,
        string userId, CancellationToken cancellationToken)
    {
        var whereQueryable = GetBaseQuery().Where(x => x.InviteeId == userId && !x.IsAccepted)
            .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));
        var orders = await whereQueryable.BuildPage(paginated, cancellationToken);
        return orders;
    }

    public async Task<bool> IsRequested(string id1, string id2)
    {
        return await base.GetBaseQuery()
            .AnyAsync(x => (x.InviteeId == id1 && x.InviterId == id2)
            || (x.InviteeId == id2 && x.InviterId == id1));
    }

    public async Task<int> GetConnectRequestsCount(string userId)
    {
        return await base.GetBaseQuery()
            .Where(x => x.InviteeId == userId && !x.IsAccepted)
            .CountAsync();
    }
    public override IQueryable<ConnectRequest> GetBaseQuery()
    {
        return base.GetBaseQuery()
            .Include(x => x.Inviter)
            .ThenInclude(x => x.Image);
    }
}