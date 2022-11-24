using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface IArtisanRepository : IRepositoryBase<Artisans>
    {
        Task<Artisans> GetArtisanByUserId(string userId);
        List<Lookup> GetArtisansWhoHaveWorkedForCustomer(string userId);
        Task<PaginatedList<Artisans>> GetArtisanPage(PaginatedCommand paginated, CancellationToken cancellationToken);
    }
}