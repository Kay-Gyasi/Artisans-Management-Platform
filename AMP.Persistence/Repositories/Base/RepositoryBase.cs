using AMP.Domain.Entities.Base;
using AMP.Domain.ViewModels;
using AMP.Persistence.Database;
using AMP.Processors.ExceptionHandlers;
using AMP.Processors.Repositories.Base;
using AMP.Shared.Domain.Models;
using AMP.Shared.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AMP.Persistence.Repositories.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private DbSet<T> _entities;
        private readonly AmpDbContext _context;
        private readonly ILogger<T> _logger;

        protected RepositoryBase(AmpDbContext context, ILogger<T> logger)
        {
            _context = context;
            _logger = logger;
        }

        protected virtual DbSet<T> Entities
        {
            get { return _entities ??= _context.Set<T>(); }
        }

        public virtual IQueryable<T> Table => Entities;
        public virtual IQueryable<T> TableNoTracking => Entities.AsNoTracking();

        public async Task<T> GetAsync(int id)
        {
            if (id <= 0) throw new InvalidIdException($"{nameof(id)} cannot be less than or equal to 0");
            var keyProperty = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties[0];
            return await GetBaseQuery().FirstOrDefaultAsync(e => EF.Property<int>(e, keyProperty.Name) == id);
        }

        public virtual IQueryable<T> GetBaseQuery()
        {
            return Entities;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await GetBaseQuery().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetBaseQuery().Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await GetBaseQuery().Where(predicate).ToListAsync(cancellationToken);
        }

        // TODO:: Incorporate offset pagination
        public async Task<PaginatedList<T>> GetPage(PaginatedCommand paginated, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(paginated.Search)) return await KeySetPaginate(paginated, null, cancellationToken);
            var predicate = GetSearchCondition(paginated.Search.ToLower());

            return await KeySetPaginate(paginated, predicate, cancellationToken);

        }

        public async Task InsertAsync(T entity, bool autoCommit = true)
        {
            try
            {
                if (entity == null)
                    throw new InvalidEntityException($"{nameof(entity)} cannot be null");

                await Entities.AddAsync(entity);
                if (autoCommit)
                    await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task InsertAsync(IEnumerable<T> entities, bool autoCommit = true)
        {
            try
            {
                if (entities == null)
                    throw new InvalidEntityException($"{nameof(entities)} cannot be null");

                foreach (var entity in entities)
                    await Entities.AddAsync(entity);
                if (autoCommit)
                    await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool autoCommit = true)
        {
            try
            {
                if (entities == null)
                    throw new InvalidEntityException($"{nameof(entities)} cannot be null");

                await Entities.AddRangeAsync(entities, cancellationToken);
                if (autoCommit)
                    await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateAsync(T entity, bool autoCommit = true)
        {
            try
            {
                if (entity == null)
                    throw new InvalidEntityException($"{nameof(entity)} cannot be null");

                Entities.Update(entity);
                if (autoCommit)
                    await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken, bool autoCommit = true)
        {
            try
            {
                if (entity == null)
                    throw new InvalidEntityException($"{nameof(entity)} cannot be null");

                Entities.Remove(entity);
                if (autoCommit)
                    await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken, bool autoCommit = true)
        {
            try
            {
                if (id <= 0)
                    throw new InvalidIdException($"{nameof(id)} cannot be less than or equal to 0");

                var keyProperty = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties[0];
                var entity = await GetBaseQuery()
                    .FirstOrDefaultAsync(e => EF.Property<int>(e, keyProperty.Name) == id, cancellationToken);
                if (entity == null)
                    throw new InvalidEntityException($"{nameof(entity)} cannot be null");

                Entities.Remove(entity);
                if (autoCommit)
                    await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        //public Task SoftDeleteAsync(T entity, bool autoCommit = true)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task DeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool autoCommit = true)
        {
            try
            {
                if (entities == null)
                    throw new InvalidEntityException($"{nameof(entities)} cannot be null");

                Entities.RemoveRange(entities);
                if (autoCommit)

                    await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        // TODO:: Work on lookup tables
        public virtual async Task<List<Lookup>> GetLookupAsync()
        {
            return new List<Lookup>();
        }




        protected virtual Expression<Func<T, bool>> GetSearchCondition(string search)
        {
            return x => x.DateCreated.ToString(CultureInfo.InvariantCulture).Contains(search);
        }


        // Write extension methods for these
        private async Task<PaginatedList<T>> KeySetPaginate(PaginatedCommand command, Expression<Func<T, bool>> predicate, CancellationToken token)
        {
            var keyProperty = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties[0];
            predicate = BuildPredicate(command, predicate, keyProperty);

            try
            {
                var query = GetBaseQuery();
                var totalCount = await query.CountAsync(token);
                var items = OrderQuery(command, query, predicate, keyProperty);
                var remaining = totalCount - await items.CountAsync(token);
                var data = items.Take(command.PageSize).ToList();
                if (command.Direction is Direction.Backward) data.Reverse();
                return new PaginatedList<T>(data, totalCount, remaining, command.PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Builds the predicate based on the last entity id and the page navigation direction.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="predicate"></param>
        /// <param name="keyProperty"></param>
        /// <returns>A predicate which specifies in which direction data will be queried</returns>
        private static Expression<Func<T, bool>> BuildPredicate(PaginatedCommand command, Expression<Func<T, bool>> predicate, IProperty keyProperty)
        {
            predicate ??= e => true; // PredicateBuilder.True<T>();

            if (command.Direction == Direction.Backward)
                predicate = predicate.And(e =>
                    EF.Property<int>(e, keyProperty.Name) < command.LastEntityId);
            else
                predicate = predicate.And(e =>
                    EF.Property<int>(e, keyProperty.Name) > command.LastEntityId);

            return predicate;
        }

        // TODO:: Work on ordering by different criteria
        /// <summary>
        /// Orders the query based on the page navigation direction.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="query"></param>
        /// <param name="predicate"></param>
        /// <param name="keyProperty"></param>
        /// <returns>An ordered query result</returns>
        private static IQueryable<T> OrderQuery(PaginatedCommand command, IQueryable<T> query, Expression<Func<T, bool>> predicate, IProperty keyProperty)
        {
            if (command.Direction == Direction.Backward)
                return query.Where(predicate)
                    .OrderByDescending(e => EF.Property<int>(e, keyProperty.Name));
            //.ThenBy(x => EF.Property<int>(x, "DateCreated"));
            else
                return query.Where(predicate)
                    .OrderBy(e => EF.Property<int>(e, keyProperty.Name));
            //.ThenBy(x => EF.Property<int>(x, "DateCreated"));
        }

    }
}
