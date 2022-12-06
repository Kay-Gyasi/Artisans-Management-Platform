using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Orders>
    {
        Task<int> GetCount(string artisanId);
        Task<List<Lookup>> GetOpenOrdersLookup(string userId);

        Task<PaginatedList<Orders>> GetSchedule(PaginatedCommand paginated, string userId,
            CancellationToken cancellationToken);

        Task<PaginatedList<Orders>> GetWorkHistory(PaginatedCommand paginated, string userId,
            CancellationToken cancellationToken);

        Task UnassignArtisan(string orderId);
        Task AssignArtisan(string orderId, string artisanId);

        Task<PaginatedList<Orders>> GetOrderHistory(PaginatedCommand paginated,
            string userId, CancellationToken cancellationToken);

        Task<PaginatedList<Orders>> GetCustomerOrderPage(PaginatedCommand paginated,
            string userId, CancellationToken cancellationToken);

        Task<PaginatedList<Orders>> GetRequests(PaginatedCommand paginated, string userId, 
            CancellationToken cancellationToken);

        Task ArtisanComplete(string orderId);

        Task Complete(string orderId);
        Task AcceptRequest(string orderId);
        Task CancelRequest(string orderId);
        Task SetCost(SetCostCommand costCommand);
        DbContext GetDbContext();
        Task DeleteAsync(string id);
    }
}