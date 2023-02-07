using AMP.Domain.Entities.UserManagement;
using AMP.Processors.Repositories.UserManagement;

namespace AMP.Persistence.Repositories.UserManagement
{
    [Repository]
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(AmpDbContext context, ILogger<Image> logger) : base(context, logger)
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