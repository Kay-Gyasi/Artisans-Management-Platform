using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Domain.Enums;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    [Repository]
    public class ImageRepository : RepositoryBase<Images>, IImageRepository
    {
        public ImageRepository(AmpDbContext context, ILogger<Images> logger) : base(context, logger)
        {
        }

        public async Task RemoveCurrentDetails(string userId)
        {
            var old = await Context.Images.FirstOrDefaultAsync(x => x.UserId == userId);
            if (old == default) return;
            old.ForUserWithId(null);
            old.EntityStatus = EntityStatus.Deleted;

            var user = await Context.Users.FindAsync(userId);
            user?.WithImageId(null);
        }
    }
}