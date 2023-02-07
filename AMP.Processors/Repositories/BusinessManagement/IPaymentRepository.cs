using AMP.Domain.Entities.BusinessManagement;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories.BusinessManagement
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<string> Verify(string reference, string trxRef);
        Task<decimal> AmountPaid(string orderId);

        Task<PaginatedList<Payment>> GetUserPage(PaginatedCommand paginated,
            string userId, string role, CancellationToken cancellationToken);

        Task<Payment> GetByTrxRef(string trxRef);
        Task<int> GetArtisanPaymentCount(string userId);
        Task<(double, double, int)> GetArtisanPaymentOverview(string userId);
        Task<(decimal, string)> GetWithdrawalDetails(string userId);
    }
}