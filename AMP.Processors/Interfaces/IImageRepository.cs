using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Interfaces.Base;

namespace AMP.Processors.Repositories
{
    public interface IImageRepository : IRepositoryBase<Images>
    {
        Task RemoveCurrentDetails(string userId);
    }
}