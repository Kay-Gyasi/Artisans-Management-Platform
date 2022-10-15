using System.Collections.Generic;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories;

public interface IInvitationRepository : IRepositoryBase<Invitations>
{
    Task<IEnumerable<Invitations>> GetUserInvites(string userId);
}