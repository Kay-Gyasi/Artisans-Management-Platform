using AMP.Domain.Entities.BusinessManagement;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories.BusinessManagement
{
    public interface IDisputeRepository : IRepository<Dispute>
    {
        DbContext GetDbContext();
        Task DeleteAsync(string id);
        Task<int> OpenDisputeCount(string userId);
        Task<PaginatedList<Dispute>> GetUserPage(PaginatedCommand paginated, string userId, CancellationToken cancellationToken);
    }
}