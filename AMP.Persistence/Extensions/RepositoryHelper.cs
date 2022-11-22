using AMP.Domain.Entities.Base;
namespace AMP.Persistence.Extensions
{
    public static class RepositoryHelper
    {
        public static async Task<PaginatedList<T>> BuildPage<T>(this IQueryable<T> whereQueryable, PaginatedCommand paginated,
            CancellationToken cancellationToken, bool orderbyDateCreated = false) where T : EntityBase
        {
            var pagedModel = whereQueryable.PageBy(x => paginated.Take, paginated);
            if (orderbyDateCreated) pagedModel = pagedModel.OrderByDescending(a => a.DateCreated);

            var totalRecords = await whereQueryable.CountAsync(cancellationToken: cancellationToken);

            return new PaginatedList<T>(data: await pagedModel.ToListAsync(cancellationToken),
                totalCount: totalRecords,
                currentPage: paginated.PageNumber,
                pageSize: paginated.PageSize);
        }

        public static string AddWhereClause(this string query) 
            => string.Join(" ", query, "where Users.EntityStatus = 'Normal' and IsSuspended = 0");

        public static string AddToWhereClause(this string query) 
            => string.Join(" ", query, "and Users.EntityStatus = 'Normal' and IsSuspended = 0");
    }
}