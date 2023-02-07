﻿using AMP.Domain.Entities.UserManagement;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories.UserManagement;

public interface IInvitationRepository : IRepository<Invitation>
{
    Task<IEnumerable<Invitation>> GetUserInvites(string userId);
}