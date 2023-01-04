using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface IPaymentRepository : IRepositoryBase<Payments>
    {
        Task<string> Verify(string reference, string trxRef);
        Task<decimal> AmountPaid(string orderId);

        Task<PaginatedList<Payments>> GetUserPage(PaginatedCommand paginated,
            string userId, string role, CancellationToken cancellationToken);

        Task<Payments> GetByTrxRef(string trxRef);
        Task<int> GetArtisanPaymentCount(string userId);
    }
}