using System.Collections.Generic;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface IServiceRepository : IRepositoryBase<Services>
    {
        Task<List<Services>> BuildServices(List<string> services);
        Task<string> GetNameAsync(string serviceId);
    }
}