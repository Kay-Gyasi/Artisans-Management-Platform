using AMP.Domain.Entities.Base;
using AMP.Processors.Exceptions;
using AMP.Processors.Repositories.Base;

namespace AMP.Persistence.Repositories.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private DbSet<T> _entities;
        protected readonly AmpDbContext Context;
        private readonly ILogger<T> _logger;

        protected RepositoryBase(AmpDbContext context, ILogger<T> logger)
        {
            Context = context;
            _logger = logger;
        }

        protected virtual DbSet<T> Entities
        {
            get { return _entities ??= Context.Set<T>(); }
        }

        public virtual IQueryable<T> Table => Entities;
        public virtual IQueryable<T> TableNoTracking => Entities.AsNoTracking();

        public async Task<T> GetAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new InvalidIdException($"{id} cannot be empty!");
            var keyProperty = Context.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties[0];
            return await GetBaseQuery().FirstOrDefaultAsync(e => EF.Property<string>(e, keyProperty.Name) == id);
        }

        public virtual IQueryable<T> GetBaseQuery() 
            => Entities;

        public virtual IQueryable<T> GetDeletedBaseQuery() 
            => Entities
                .Where(x => x.EntityStatus == EntityStatus.Deleted)
                .IgnoreQueryFilters();

        public virtual IQueryable<T> GetArchivedBaseQuery() 
            => Entities
                .Where(x => x.EntityStatus == EntityStatus.Archived)
                .IgnoreQueryFilters();

        public async Task<List<T>> GetAllAsync() 
            => await GetBaseQuery().ToListAsync();

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate) 
            => await GetBaseQuery().Where(predicate).ToListAsync();

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) 
            => await GetBaseQuery().Where(predicate).ToListAsync(cancellationToken);

        public async Task<List<T>> GetAllDeletedAsync() 
            => await GetDeletedBaseQuery().ToListAsync();

        public async Task<List<T>> GetAllArchivedAsync() 
            => await GetArchivedBaseQuery().ToListAsync();

        public virtual async Task<PaginatedList<T>> GetPage(PaginatedCommand paginated, CancellationToken cancellationToken)
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
                if (entities is null)
                    throw new InvalidEntityException($"{nameof(entities)} cannot be null");

                foreach (var entity in entities)
                    await Entities.AddAsync(entity);
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
                if (entities is null)
                    throw new InvalidEntityException($"{nameof(entities)} cannot be null");

                await Entities.AddRangeAsync(entities, cancellationToken);
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
                if (entity is null)
                    throw new InvalidEntityException($"{nameof(entity)} cannot be null");

                await Task.Run(() => Entities.Update(entity));
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
                if (entity is null)
                    throw new InvalidEntityException($"{nameof(entity)} cannot be null");

                await Task.Run(() => Entities.Remove(entity), cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred at DeleteAsync for {nameof(T)}: " + ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new InvalidIdException($"Id cannot be empty");

                var keyProperty = Context.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties[0];
                var entity = await GetBaseQuery()
                    .FirstOrDefaultAsync(e => EF.Property<string>(e, keyProperty.Name) == id, cancellationToken);
                if (entity is null)
                    throw new InvalidIdException($"{nameof(T)} with id: {id} does not exist");

                await Task.Run(() => Entities.Remove(entity), cancellationToken);
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

                await Task.Run(() => Entities.RemoveRange(entities), cancellationToken);
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
                await Task.Run(() => Entities.Update(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting {nameof(T)}: " + ex.Message);
                throw;
            }
        }

        public async Task<int> CountAsync() 
            => await Context.Set<T>().CountAsync();

        // TODO:: Remove async from method
        public virtual async Task<List<Lookup>> GetLookupAsync()
        {
            await Task.CompletedTask;
            return new List<Lookup>();
        }

        protected virtual Expression<Func<T, bool>> GetSearchCondition(string search)
            => x => true;
    }
}
