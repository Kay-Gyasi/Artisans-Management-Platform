using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Shared.Domain.Models;
using AMP.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AMP.Persistence.Extensions
{
    public static class RepositoryHelper
    {
        public static async Task<PaginatedList<T>> BuildPage<T>(this IQueryable<T> whereQueryable, PaginatedCommand paginated,
            CancellationToken cancellationToken)
        {
            var pagedModel = await whereQueryable.PageBy(x => paginated.Take, paginated)
                .ToListAsync(cancellationToken);

            var totalRecords = await whereQueryable.CountAsync(cancellationToken: cancellationToken);


            return new PaginatedList<T>(data: pagedModel,
                totalCount: totalRecords,
                currentPage: paginated.PageNumber,
                pageSize: paginated.PageSize);
        }   
    }
}