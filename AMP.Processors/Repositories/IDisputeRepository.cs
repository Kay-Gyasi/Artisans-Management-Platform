using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface IDisputeRepository : IRepositoryBase<Disputes>
    {
        DbContext GetDbContext();
        Task DeleteAsync(string id);
        Task<int> OpenDisputeCount(string userId);
        Task<PaginatedList<Disputes>> GetUserPage(PaginatedCommand paginated, string userId, CancellationToken cancellationToken);
    }
}