using AMP.Domain.Entities.UserManagement;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories.UserManagement
{
    public interface IArtisanRepository : IRepositoryBase<Artisan>
    {
        Task DeleteAsync(string id);
        Task<Artisan> GetArtisanByUserId(string userId);
        Task<List<Lookup>> GetArtisansWhoHaveWorkedForCustomer(string userId);
        Task<PaginatedList<Artisan>> GetArtisanPage(PaginatedCommand paginated, CancellationToken cancellationToken);
    }
}