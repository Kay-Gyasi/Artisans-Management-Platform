using System.Linq;
using AMP.Domain.Entities;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    [Repository]
    public class UserRepository : RepositoryBase<Users>, IUserRepository
    {
        public UserRepository(AmpDbContext context, ILogger<Users> logger) : base(context, logger)
        {
        }

        public override IQueryable<Users> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.Languages);
        }
    }
}