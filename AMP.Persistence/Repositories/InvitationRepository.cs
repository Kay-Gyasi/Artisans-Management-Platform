using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.Extensions.Logging;

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