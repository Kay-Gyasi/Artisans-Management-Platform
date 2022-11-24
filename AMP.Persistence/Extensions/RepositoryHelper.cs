using AMP.Domain.Entities.Base;
namespace AMP.Persistence.Extensions
{
    public static class RepositoryHelper
    {
        public static async Task<PaginatedList<T>> BuildPage<T>(this IQueryable<T> whereQueryable, PaginatedCommand paginated,
            CancellationToken cancellationToken) where T : EntityBase
        {
            var pagedModel = whereQueryable.PageBy(x => paginated.Take, paginated);
            var totalRecords = await whereQueryable.CountAsync(cancellationToken: cancellationToken);

            return new PaginatedList<T>(data: await pagedModel.ToListAsync(cancellationToken),
                totalCount: totalRecords,
                currentPage: paginated.PageNumber,
                pageSize: paginated.PageSize);
        }

        public static string AddBaseFilter(this string query) 
            => string.Join(" ", query, "WHERE EntityStatus = 'Normal'");

        public static string AddBaseFilterToWhereClause(this string query) 
            => string.Join(" ", query, "AND EntityStatus = 'Normal'");
    }
}