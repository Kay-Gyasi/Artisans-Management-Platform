using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface IPaymentRepository : IRepositoryBase<Payments>
    {
        Task Verify(string reference, string trxRef);
        Task<decimal> AmountPaid(string orderId);
    }
}