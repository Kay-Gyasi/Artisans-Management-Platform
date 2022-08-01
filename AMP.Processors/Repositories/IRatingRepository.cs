using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface IRatingRepository : IRepositoryBase<Ratings>
    {
        double GetRating(int artisanId);
        int GetCount(int artisanId);
    }
}