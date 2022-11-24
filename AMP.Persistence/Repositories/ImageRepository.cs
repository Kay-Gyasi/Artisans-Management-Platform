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