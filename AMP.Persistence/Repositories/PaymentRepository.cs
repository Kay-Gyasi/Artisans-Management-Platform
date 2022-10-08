using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Persistence.Database;
using AMP.Persistence.Extensions;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.ExceptionHandlers;
using AMP.Processors.Interfaces;
using AMP.Shared.Domain.Models;
using AMP.Shared.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    [Repository]
    public class PaymentRepository : RepositoryBase<Payments>, IPaymentRepository
    {
        private readonly ILogger<Payments> _logger;

        public PaymentRepository(AmpDbContext context, ILogger<Payments> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<decimal> AmountPaid(string orderId) =>
            await Task.Run(() => GetBaseQuery().Where(x => x.OrderId == orderId)
                .Sum(x => x.AmountPaid));

        public async Task Verify(string reference, string trxRef)
        {
            var payment = await GetBaseQuery().FirstOrDefaultAsync(x => x.Reference == reference);
            payment?.HasBeenVerified(true);
            payment?.WithTransactionReference(trxRef);
        }

        public async Task<PaginatedList<Payments>> GetUserPage(PaginatedCommand paginated, 
            string userId, string role, CancellationToken cancellationToken)
        {
            var typeId = await GetUserTypeId(userId, role);

            var whereQueryable = role == "Artisan"
                ? GetBaseQuery().Where(x => x.Order.ArtisanId == typeId)
                    .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search))
                : GetBaseQuery().Where(x => x.Order.CustomerId == typeId)
                    .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));

            return await whereQueryable.BuildPage(paginated, cancellationToken);
        }

        public override IQueryable<Payments> GetBaseQuery() =>
            base.GetBaseQuery()
                .Include(x => x.Order);

        protected override Expression<Func<Payments, bool>> GetSearchCondition(string search)
        {
            return x => x.Order.Description.Contains(search)
                        || x.Reference.Equals(search)
                        || x.AmountPaid.ToString(CultureInfo.InvariantCulture).Equals(search);
        }

        private async Task<string> GetUserTypeId(string userId, string role)
        {
            try
            {
                var userTypeId = role == "Artisan"
                    ? (await Context.Artisans.FirstOrDefaultAsync(x => x.UserId == userId))?.Id
                    : (await Context.Customers.FirstOrDefaultAsync(x => x.UserId == userId))?.Id;
                if (string.IsNullOrEmpty(userTypeId)) throw new UserTypeIdNotFoundException();
                return userTypeId;
            }
            catch (UserTypeIdNotFoundException ex)
            {
                _logger.LogError($"User with id {userId} not found in types \n {ex.Message}");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}");
                throw;
            }
        }
    }
}