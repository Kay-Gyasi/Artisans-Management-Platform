using AMP.Domain.Entities;
using AMP.Domain.Enums;
using AMP.Domain.ViewModels;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using AMP.Shared.Domain.Models;
using AMP.Shared.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AMP.Persistence.Repositories
{
    [Repository]
    public class OrderRepository : RepositoryBase<Orders>, IOrderRepository
    {
        public OrderRepository(AmpDbContext context, ILogger<Orders> logger) : base(context, logger)
        {
        }

        public Task<List<Lookup>> GetOpenOrdersLookup(int userId)
        {
            return GetBaseQuery().Where(x => x.Customer.UserId == userId && !x.IsComplete).Select(x => new Lookup()
                {
                    Id = x.Id,
                    Name = x.Description
                }).OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task Complete(int orderId)
        {
            var order = await GetBaseQuery().FirstOrDefaultAsync(x => x.Id == orderId);
            order.WithStatus(OrderStatus.Completed)
                .IsCompleted(true);
            await UpdateAsync(order);
        }

        public async Task AcceptRequest(int orderId)
        {
            var order = await GetBaseQuery().FirstOrDefaultAsync(x => x.Id == orderId);
            order.RequestAccepted(true);
            await UpdateAsync(order);
        }
        
        public async Task CancelRequest(int orderId)
        {
            var order = await GetBaseQuery().FirstOrDefaultAsync(x => x.Id == orderId);
            order.RequestAccepted(false);
            await UpdateAsync(order);
        }

        public async Task UnassignArtisan(int orderId)
        {
            var order = await GetBaseQuery().FirstOrDefaultAsync(x => x.Id == orderId);
            order.ForArtisanWithId(null);
            await UpdateAsync(order);
        }

        public async Task AssignArtisan(int orderId, int artisanId)
        {
            var order = await GetBaseQuery().FirstOrDefaultAsync(x => x.Id == orderId);
            order.ForArtisanWithId(artisanId);
            await UpdateAsync(order);

            await _context.Requests.AddAsync(Requests.Create(order.CustomerId, artisanId, orderId));
        }

        public async Task<PaginatedList<Orders>> GetCustomerOrderPage(PaginatedCommand paginated,
            int userId, CancellationToken cancellationToken)
        {
            var whereQueryable = GetBaseQuery().Where(x => x.Status != OrderStatus.Completed && x.Customer.UserId == userId)
                .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));

            return await BuildPage(whereQueryable, paginated, cancellationToken);
        }

        public async Task<PaginatedList<Orders>> GetOrderHistory(PaginatedCommand paginated,
            int userId, CancellationToken cancellationToken)
        {
            var whereQueryable = GetBaseQuery().Where(x => x.Customer.UserId == userId && x.Status == OrderStatus.Completed)
                .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));

            return await BuildPage(whereQueryable, paginated, cancellationToken);
        }

        public async Task<PaginatedList<Orders>> GetSchedule(PaginatedCommand paginated, int userId, 
            CancellationToken cancellationToken)
        {
            var whereQueryable = GetBaseQuery().Where(x => x.Artisan.UserId == userId 
                                                           && x.Status != OrderStatus.Completed
                                                           && x.IsRequestAccepted)
                .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search))
                .OrderBy(x => x.PreferredDate);

            return await BuildPage(whereQueryable, paginated, cancellationToken);
        }
        
        public async Task<PaginatedList<Orders>> GetRequests(PaginatedCommand paginated, int userId, 
            CancellationToken cancellationToken)
        {
            var whereQueryable = GetBaseQuery().Where(x => x.Artisan.UserId == userId 
                                                           && x.Status != OrderStatus.Completed
                                                           && !x.IsRequestAccepted)
                .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search))
                .OrderBy(x => x.PreferredDate);

            return await BuildPage(whereQueryable, paginated, cancellationToken);
        }

        public async Task<PaginatedList<Orders>> GetWorkHistory(PaginatedCommand paginated, int userId
            , CancellationToken cancellationToken)
        {
            var whereQueryable = GetBaseQuery().Where(x => x.Artisan.UserId == userId && x.Status == OrderStatus.Completed)
                .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search))
                .OrderBy(x => x.PreferredDate);

            return await BuildPage(whereQueryable, paginated, cancellationToken);
        }

        public int GetCount(int artisanId)
        {
            return GetBaseQuery().Count(x => x.ArtisanId == artisanId);
        }

        public override IQueryable<Orders> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.WorkAddress)
                .Include(x => x.Artisan)
                .Include(x => x.Service)
                .Include(x => x.Payment)
                .Include(x => x.Customer)
                .ThenInclude(a => a.User);
        }

        // NOTE:: .StartsWith() => search%, .Contains() => %search%
        protected override Expression<Func<Orders, bool>> GetSearchCondition(string search)
        {
            
            return x => x.Description.ToLower().Contains(search.ToLower())
                        || x.Service.Name == search
                        || x.WorkAddress.City.ToLower().Contains(search.ToLower())
                        || x.WorkAddress.StreetAddress.ToLower().Contains(search.ToLower())
                        || x.WorkAddress.StreetAddress2.ToLower().Contains(search.ToLower());
            
        }

        public override Task<List<Lookup>> GetLookupAsync()
        {
            return GetBaseQuery().Select(x => new Lookup()
            {
                Id = x.Id,
                Name = x.Description
            }).OrderBy(x => x.Name)
                .ToListAsync();
        }

        private async Task<PaginatedList<Orders>> BuildPage(IQueryable<Orders> whereQueryable, PaginatedCommand paginated,
            CancellationToken cancellationToken)
        {
            var pagedModel = await whereQueryable.PageBy(x => paginated.Take, paginated)
                .ToListAsync(cancellationToken);

            var totalRecords = await whereQueryable.CountAsync(cancellationToken: cancellationToken);


            return new PaginatedList<Orders>(data: pagedModel,
                totalCount: totalRecords,
                currentPage: paginated.PageNumber,
                pageSize: paginated.PageSize);
        }
    }
}