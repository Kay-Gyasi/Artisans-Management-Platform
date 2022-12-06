using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface IArtisanRepository : IRepositoryBase<Artisans>
    {
        Task DeleteAsync(string id);
        Task<Artisans> GetArtisanByUserId(string userId);
        Task<List<Lookup>> GetArtisansWhoHaveWorkedForCustomer(string userId);
        Task<PaginatedList<Artisans>> GetArtisanPage(PaginatedCommand paginated, CancellationToken cancellationToken);
    }
}