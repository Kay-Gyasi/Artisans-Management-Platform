using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface ICustomerRepository : IRepositoryBase<Customers>
    {
        Task DeleteAsync(string id);
        Task<Customers> GetByUserIdAsync(string userId);
        Task<string> GetCustomerId(string userId);
    }
}