using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface IDisputeRepository : IRepositoryBase<Disputes>
    {
        Task<int> OpenDisputeCount(string userId);
    }
}