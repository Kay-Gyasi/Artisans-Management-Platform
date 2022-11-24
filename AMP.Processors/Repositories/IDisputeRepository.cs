using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface IDisputeRepository : IRepositoryBase<Disputes>
    {
        Task<int> OpenDisputeCount(string userId);
        Task<PaginatedList<Disputes>> GetUserPage(PaginatedCommand paginated, string userId, CancellationToken cancellationToken);
    }
}