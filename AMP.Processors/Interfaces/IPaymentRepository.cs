using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Interfaces.Base;
using AMP.Shared.Domain.Models;

namespace AMP.Processors.Interfaces
{
    public interface IPaymentRepository : IRepositoryBase<Payments>
    {
        Task Verify(string reference, string trxRef);
        Task<decimal> AmountPaid(string orderId);

        Task<PaginatedList<Payments>> GetUserPage(PaginatedCommand paginated,
            string userId, string role, CancellationToken cancellationToken);
    }
}