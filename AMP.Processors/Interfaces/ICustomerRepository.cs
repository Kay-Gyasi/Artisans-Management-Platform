using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Interfaces.Base;

namespace AMP.Processors.Repositories
{
    public interface ICustomerRepository : IRepositoryBase<Customers>
    {
        Task<Customers> GetByUserIdAsync(string userId);
        Task<string> GetCustomerId(string userId);
    }
}