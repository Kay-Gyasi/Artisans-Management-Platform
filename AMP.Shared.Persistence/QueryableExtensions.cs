using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using AMP.Shared.Domain.Models;

namespace AMP.Shared.Persistence
{
	public static class QueryableExtensions
	{
		public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
		{
			try
			{
				return condition ? query.Where(predicate) : query;

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public static IQueryable<T> PageBy<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> orderBy,
			int page,
			int pageSize,
			bool? orderByDescending = true)
		{
			const int defaultPageNumber = 1;

			if (query == null)
			{
				throw new ArgumentNullException(nameof(query));
			}

			// Check if the page number is greater then zero - otherwise use default page number
			if (page <= 0)
			{
				page = defaultPageNumber;
			}


			if (orderByDescending != null)
			{
				// It is necessary sort items before it
				query = orderByDescending ?? false ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
			}

			int skip = (page - 1) * pageSize;
			return query.Skip(skip).Take(pageSize);
		}

		public static IQueryable<T> PageBy<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> orderBy,
			PaginatedCommand paginated)
		{
			const int defaultPageNumber = 1;

			if (query == null)
			{
				throw new ArgumentNullException(nameof(query));
			}

			// Check if the page number is greater then zero - otherwise use default page number

			if (paginated.PageNumber <= 0)
			{
				paginated.PageNumber = defaultPageNumber;
			}


			//if (orderByDescending != null)
			//{
			//	// It is necessary sort items before it
			//	query = orderByDescending ?? false ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
			//}

			//int skip = (page - 1) * pageSize;
			return query.Skip(paginated.Skip).Take(paginated.PageSize);
		}
	}

}