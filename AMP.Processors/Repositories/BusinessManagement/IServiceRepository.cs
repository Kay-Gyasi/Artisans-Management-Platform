using AMP.Domain.Entities.BusinessManagement;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories.BusinessManagement
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<List<Service>> BuildServices(IEnumerable<string> services);
        Task<string> GetNameAsync(string serviceId);
        Task<List<Lookup>> GetAvailableServices();
    }
}