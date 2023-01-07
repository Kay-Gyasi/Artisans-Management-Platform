using AMP.Domain.Entities.UserManagement;
using AMP.Processors.Repositories.UserManagement;

namespace AMP.Persistence.Repositories.UserManagement;

[Repository]
public class InvitationRepository : RepositoryBase<Invitation>, IInvitationRepository
{
    public InvitationRepository(AmpDbContext context, ILogger<Invitation> logger) : base(context, logger)
    {
    }

    public async Task<IEnumerable<Invitation>> GetUserInvites(string userId)
    {
        return await Task.Run(() => GetBaseQuery().Where(x => x.UserId == userId));
    }
}