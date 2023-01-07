using AMP.Domain.Entities.BusinessManagement;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories.BusinessManagement
{
    public interface IRatingRepository : IRepositoryBase<Rating>
    {
        double GetRating(string artisanId);
        int GetCount(string artisanId);
        Task DeletePreviousRatingForSameArtisan(string customerId, string artisanId);

        Task<PaginatedList<Rating>> GetArtisanRatingPage(PaginatedCommand paginated, string userId,
            CancellationToken cancellationToken);
        Task<PaginatedList<Rating>> GetCustomerRatingPage(PaginatedCommand paginated, string userId,
            CancellationToken cancellationToken);
    }
}