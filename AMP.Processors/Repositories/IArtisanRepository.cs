using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Domain.ViewModels;
using AMP.Processors.Repositories.Base;
using AMP.Shared.Domain.Models;

namespace AMP.Processors.Repositories
{
    public interface IArtisanRepository : IRepositoryBase<Artisans>
    {
        Task<Artisans> GetArtisanByUserId(string userId);
        List<Lookup> GetArtisansWhoHaveWorkedForCustomer(string userId);
        Task<PaginatedList<Artisans>> GetArtisanPage(PaginatedCommand paginated, CancellationToken cancellationToken);
    }
}