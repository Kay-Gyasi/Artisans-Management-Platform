using AMP.Domain.Entities.Base;
using AMP.Domain.Enums;
using AMP.Domain.ViewModels;
using AMP.Persistence.Database;
using AMP.Processors.ExceptionHandlers;
using AMP.Processors.Repositories.Base;
using AMP.Shared.Domain.Models;
using AMP.Shared.Persistence;
using Microsoft.EntityFrameworkCore;
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
            return Entities.Where(x => x.EntityStatus == EntityStatus.Normal);
        }
        public virtual IQueryable<T> GetDeletedBaseQuery()
        {
            return Entities.Where(x => x.EntityStatus == EntityStatus.Deleted);
        }
        public virtual IQueryable<T> GetArchivedBaseQuery()
        {
            return Entities.Where(x => x.EntityStatus == EntityStatus.Archived);
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
        public async Task<List<T>> GetAllDeletedAsync()
        {
            return await GetDeletedBaseQuery().ToListAsync();
        }
        public async Task<List<T>> GetAllArchivedAsync()
        {
            return await GetArchivedBaseQuery().ToListAsync();
        }

        public async Task<PaginatedList<T>> GetPage(PaginatedCommand paginated, CancellationToken cancellationToken)
        {


            var whereQueryable = GetBaseQuery()
                .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));

            var pagedModel = await whereQueryable.PageBy(x => paginated.Take, paginated)
                .ToListAsync(cancellationToken);

            var totalRecords = await whereQueryable.CountAsync(cancellationToken: cancellationToken);


            return new PaginatedList<T>(data: pagedModel,
                totalCount: totalRecords,
                currentPage: paginated.PageNumber,
                pageSize: paginated.PageSize);
        }

        public async Task InsertAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new InvalidEntityException($"{nameof(entity)} cannot be null");

                await Entities.AddAsync(entity);
                //if (autoCommit)
                    //await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred at InsertAsync for {nameof(T)}: " + ex.Message);
                throw;
            }
        }

        public async Task InsertAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new InvalidEntityException($"{nameof(entities)} cannot be null");

                foreach (var entity in entities)
                    await Entities.AddAsync(entity);
                //if (autoCommit)
                //    await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred at InsertAsync for IEnumerable<{nameof(T)}>: " + ex.Message);
                throw;
            }
        }
        public async Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            try
            {
                if (entities == null)
                    throw new InvalidEntityException($"{nameof(entities)} cannot be null");

                await Entities.AddRangeAsync(entities, cancellationToken);
                //if (autoCommit)
                //    await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred at InsertAsync for IEnumerable<{nameof(T)}>: " + ex.Message);
                throw;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new InvalidEntityException($"{nameof(entity)} cannot be null");

                Entities.Update(entity);
                //if (autoCommit)
                //    await _context.SaveChangesAsync();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred at UpdateAsync for {nameof(T)}: " + ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            try
            {
                if (entity == null)
                    throw new InvalidEntityException($"{nameof(entity)} cannot be null");

                Entities.Remove(entity);
                //if (autoCommit)
                //    await _context.SaveChangesAsync(cancellationToken);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred at DeleteAsync for {nameof(T)}: " + ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
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
                //if (autoCommit)
                //    await _context.SaveChangesAsync(cancellationToken);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred at DeleteAsync with id: " + ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            try
            {
                if (entities == null)
                    throw new InvalidEntityException($"{nameof(entities)} cannot be null");

                Entities.RemoveRange(entities);
                //if (autoCommit)
                //    await _context.SaveChangesAsync(cancellationToken);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred at DeleteAsync for IEnumerable<{nameof(T)}>: " + ex.Message);
                throw;
            }
        }

        public async Task SoftDeleteAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new InvalidEntityException($"{nameof(entity)} cannot be null");

                entity.EntityStatus = EntityStatus.Deleted;
                await UpdateAsync(entity);
                //if (autoCommit)
                //    await _context.SaveChangesAsync();
                //await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred at SoftDeleteAsync for {nameof(T)}: " + ex.Message);
                throw;
            }
        }

        //public async Task CommitAsync()
        //{
        //    await _context.SaveChangesAsync();
        //}

        //public async Task CommitAsync(CancellationToken cancellationToken)
        //{
        //    await _context.SaveChangesAsync(cancellationToken);
        //}

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public virtual async Task<List<Lookup>> GetLookupAsync()
        {
            return new List<Lookup>();
        }




        protected virtual Expression<Func<T, bool>> GetSearchCondition(string search)
        {
            return x => x.DateCreated.ToString(CultureInfo.InvariantCulture).Contains(search);
        }

    }
}
