using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Repositories.Base;
using AMP.Shared.Domain.Models;

namespace AMP.Processors.Repositories
{
    public interface IRatingRepository : IRepositoryBase<Ratings>
    {
        double GetRating(int artisanId);
        int GetCount(int artisanId);
        Task OverridePreviousRating(int customerId, int artisanId);

        Task<PaginatedList<Ratings>> GetArtisanRatingPage(PaginatedCommand paginated, int userId,
            CancellationToken cancellationToken);
    }
}