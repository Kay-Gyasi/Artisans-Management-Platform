using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Repositories.Base;
using AMP.Shared.Domain.Models;

namespace AMP.Processors.Repositories
{
    public interface IRatingRepository : IRepositoryBase<Ratings>
    {
        double GetRating(string artisanId);
        int GetCount(string artisanId);
        Task OverridePreviousRating(string customerId, string artisanId);

        Task<PaginatedList<Ratings>> GetArtisanRatingPage(PaginatedCommand paginated, string userId,
            CancellationToken cancellationToken);
        Task<PaginatedList<Ratings>> GetCustomerRatingPage(PaginatedCommand paginated, string userId,
            CancellationToken cancellationToken);
    }
}