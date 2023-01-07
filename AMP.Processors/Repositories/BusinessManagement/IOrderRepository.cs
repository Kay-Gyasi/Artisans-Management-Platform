using AMP.Domain.Entities.BusinessManagement;
using AMP.Processors.Commands.BusinessManagement;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories.BusinessManagement
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<int> GetScheduleCount(string userId);
        Task<int> GetJobRequestsCount(string userId);
        Task<int> GetCount(string artisanId);
        Task<List<Lookup>> GetOpenOrdersLookup(string userId);

        Task<PaginatedList<Order>> GetSchedule(PaginatedCommand paginated, string userId,
            CancellationToken cancellationToken);

        Task<PaginatedList<Order>> GetWorkHistory(PaginatedCommand paginated, string userId,
            CancellationToken cancellationToken);

        Task<string> UnassignArtisan(string orderId);
        Task<string> AssignArtisan(string orderId, string artisanId);

        Task<PaginatedList<Order>> GetOrderHistory(PaginatedCommand paginated,
            string userId, CancellationToken cancellationToken);

        Task<PaginatedList<Order>> GetCustomerOrderPage(PaginatedCommand paginated,
            string userId, CancellationToken cancellationToken);

        Task<PaginatedList<Order>> GetRequests(PaginatedCommand paginated, string userId, 
            CancellationToken cancellationToken);

        Task<string> ArtisanComplete(string orderId);

        Task Complete(string orderId);
        Task<string> AcceptRequest(string orderId);
        Task<string> CancelRequest(string orderId);
        Task SetCost(SetCostCommand costCommand);
        DbContext GetDbContext();
        Task DeleteAsync(string id);
    }
}