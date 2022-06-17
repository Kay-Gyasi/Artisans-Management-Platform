using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.ViewModels;
using AMP.Shared.Domain.Models;

namespace AMP.Processors.Repositories.Base
{
	public interface IRepositoryBase<T>
	{
		IQueryable<T> Table { get; }
		IQueryable<T> TableNoTracking { get; }
		Task CommitAsync();
		Task CommitAsync(CancellationToken cancellationToken);
		Task DeleteAsync(T entity, CancellationToken cancellationToken, bool autoCommit = true);
		Task DeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool autoCommit = true);
		Task DeleteAsync(int id, CancellationToken cancellationToken, bool autoCommit = true);
		Task<T> GetAsync(int id);
		Task<List<T>> GetAllAsync();
		Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
		Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
		Task<PaginatedList<T>> GetPage(PaginatedCommand paginated, CancellationToken cancellationToken);
		Task<List<Lookup>> GetLookupAsync();
		Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool autoCommit = true);
		Task InsertAsync(IEnumerable<T> entities, bool autoCommit = true);
		Task InsertAsync(T entity, bool autoCommit = true);
		Task UpdateAsync(T entity, bool autoCommit = true);
		IQueryable<T> GetBaseQuery();
		IQueryable<T> GetDeletedBaseQuery();
		IQueryable<T> GetArchivedBaseQuery();
		Task<List<T>> GetAllDeletedAsync();
		Task<List<T>> GetAllArchivedAsync();
		Task SoftDeleteAsync(T entity, bool autoCommit = true);
		Task<int> CountAsync();
	}
}
