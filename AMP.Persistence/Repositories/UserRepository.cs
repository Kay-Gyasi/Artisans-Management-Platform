using AMP.Domain.Entities;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<Users>, IUserRepository
    {
        protected UserRepository(AmpDbContext context, ILogger<Users> logger) : base(context, logger)
        {
        }
    }
}