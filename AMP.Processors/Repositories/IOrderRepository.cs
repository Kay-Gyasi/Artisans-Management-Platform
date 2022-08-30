using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Domain.ViewModels;
using AMP.Processors.Commands;
using AMP.Processors.Repositories.Base;
using AMP.Shared.Domain.Models;

namespace AMP.Processors.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Orders>
    {
        int GetCount(int artisanId);
        Task<List<Lookup>> GetOpenOrdersLookup(int userId);

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

        Task ArtisanComplete(int orderId);

        Task Complete(int orderId);
        Task AcceptRequest(int orderId);
        Task CancelRequest(int orderId);
        Task SetCost(SetCostCommand costCommand);
    }
}