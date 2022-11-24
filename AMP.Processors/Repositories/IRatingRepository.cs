using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface IRatingRepository : IRepositoryBase<Ratings>
    {
        double GetRating(string artisanId);
        int GetCount(string artisanId);
        Task DeletePreviousRatingForSameArtisan(string customerId, string artisanId);

        Task<PaginatedList<Ratings>> GetArtisanRatingPage(PaginatedCommand paginated, string userId,
            CancellationToken cancellationToken);
        Task<PaginatedList<Ratings>> GetCustomerRatingPage(PaginatedCommand paginated, string userId,
            CancellationToken cancellationToken);
    }
}