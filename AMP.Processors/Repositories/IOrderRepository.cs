using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Orders>
    {
        Task<int> GetScheduleCount(string userId);
        Task<int> GetJobRequestsCount(string userId);
        Task<int> GetCount(string artisanId);
        Task<List<Lookup>> GetOpenOrdersLookup(string userId);

        Task<PaginatedList<Orders>> GetSchedule(PaginatedCommand paginated, string userId,
            CancellationToken cancellationToken);

        Task<PaginatedList<Orders>> GetWorkHistory(PaginatedCommand paginated, string userId,
            CancellationToken cancellationToken);

        Task<string> UnassignArtisan(string orderId);
        Task<string> AssignArtisan(string orderId, string artisanId);

        Task<PaginatedList<Orders>> GetOrderHistory(PaginatedCommand paginated,
            string userId, CancellationToken cancellationToken);

        Task<PaginatedList<Orders>> GetCustomerOrderPage(PaginatedCommand paginated,
            string userId, CancellationToken cancellationToken);

        Task<PaginatedList<Orders>> GetRequests(PaginatedCommand paginated, string userId, 
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