namespace AMP.Persistence.Repositories;

[Repository]
public class InvitationRepository : RepositoryBase<Invitations>, IInvitationRepository
{
    public InvitationRepository(AmpDbContext context, ILogger<Invitations> logger) : base(context, logger)
    {
    }

    public async Task<IEnumerable<Invitations>> GetUserInvites(string userId)
    {
        return await Task.Run(() => GetBaseQuery().Where(x => x.UserId == userId));
    }
}