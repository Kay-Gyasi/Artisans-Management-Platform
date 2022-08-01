using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface IArtisanRepository : IRepositoryBase<Artisans>
    {
        Task<Artisans> GetArtisanByUserId(int userId);
    }
}