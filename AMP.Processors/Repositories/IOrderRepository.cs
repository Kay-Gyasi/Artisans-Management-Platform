using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Repositories.Base;
using AMP.Shared.Domain.Models;

namespace AMP.Processors.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Orders>
    {
        int GetCount(int artisanId);

        Task<PaginatedList<Orders>> GetSchedule(PaginatedCommand paginated, int userId,
            CancellationToken cancellationToken);

        Task<PaginatedList<Orders>> GetWorkHistory(PaginatedCommand paginated, int userId,
            CancellationToken cancellationToken);

        Task UnassignArtisan(int orderId);
        Task AssignArtisan(int orderId, int artisanId);

        Task<PaginatedList<Orders>> GetOrderHistory(PaginatedCommand paginated,
            int userId, CancellationToken cancellationToken);

        Task<PaginatedList<Orders>> GetCustomerOrderPage(PaginatedCommand paginated,
            int userId, CancellationToken cancellationToken);

        Task<PaginatedList<Orders>> GetRequests(PaginatedCommand paginated, int userId, 
            CancellationToken cancellationToken);

        Task Complete(int orderId);
        Task AcceptRequest(int orderId);
        Task CancelRequest(int orderId);
    }
}