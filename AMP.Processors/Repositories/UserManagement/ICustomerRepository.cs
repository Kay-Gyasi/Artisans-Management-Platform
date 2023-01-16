using AMP.Domain.Entities.UserManagement;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories.UserManagement
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task SoftDeleteAsync(string id);
        Task<Customer> GetByUserIdAsync(string userId);
        Task<string> GetCustomerId(string userId);
    }
}