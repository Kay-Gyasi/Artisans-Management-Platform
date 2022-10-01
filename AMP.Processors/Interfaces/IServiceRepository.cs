using System.Collections.Generic;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Domain.ViewModels;
using AMP.Processors.Interfaces.Base;

namespace AMP.Processors.Repositories
{
    public interface IServiceRepository : IRepositoryBase<Services>
    {
        Task<List<Services>> BuildServices(List<string> services);
        Task<string> GetNameAsync(string serviceId);
        Task<List<Lookup>> GetAvailableServices();
    }
}