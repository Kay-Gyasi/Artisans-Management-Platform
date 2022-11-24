using System.Linq.Expressions;

namespace AMP.Processors.Repositories.Base
{
	public interface IRepositoryBase<T>
	{
		IQueryable<T> Table { get; }
		IQueryable<T> TableNoTracking { get; }
		//Task CommitAsync();
		//Task CommitAsync(CancellationToken cancellationToken);
		Task DeleteAsync(T entity, CancellationToken cancellationToken);
		Task DeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
		Task DeleteAsync(string id, CancellationToken cancellationToken);
		Task<T> GetAsync(string id);
		Task<List<T>> GetAllAsync();
		Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
		Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
		Task<PaginatedList<T>> GetPage(PaginatedCommand paginated, CancellationToken cancellationToken);
		Task<List<Lookup>> GetLookupAsync();
		Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
		Task InsertAsync(IEnumerable<T> entities);
		Task InsertAsync(T entity);
		Task UpdateAsync(T entity);
		IQueryable<T> GetBaseQuery();
		IQueryable<T> GetDeletedBaseQuery();
		IQueryable<T> GetArchivedBaseQuery();
		Task<List<T>> GetAllDeletedAsync();
		Task<List<T>> GetAllArchivedAsync();
		Task SoftDeleteAsync(T entity);
		Task<int> CountAsync();
	}
}
