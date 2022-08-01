using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface ICustomerRepository : IRepositoryBase<Customers>
    {
        Task<Customers> GetByUserIdAsync(int userId);
        Task<int> GetCustomerId(int userId);
    }
}