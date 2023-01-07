using System.Globalization;
using AMP.Domain.Entities.BusinessManagement;
using AMP.Processors.Repositories.BusinessManagement;

namespace AMP.Persistence.Repositories.BusinessManagement
{
    [Repository]
    public class PaymentRepository : RepositoryBase<Payment>, IPaymentRepository
    {
        private readonly ILogger<Payment> _logger;

        public PaymentRepository(AmpDbContext context, ILogger<Payment> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<decimal> AmountPaid(string orderId) =>
            await Task.Run(() => GetBaseQuery().Where(x => x.OrderId == orderId && x.IsVerified)
                .Sum(x => x.AmountPaid));

        public async Task<string> Verify(string reference, string trxRef)
        {
            var payment = await GetBaseQuery()
                .FirstOrDefaultAsync(x => x.Reference == reference);
            payment?.HasBeenVerified(true);
            payment?.WithTransactionReference(trxRef);

            return payment?.Order.Artisan.UserId;
        }

        public async Task<PaginatedList<Payment>> GetUserPage(PaginatedCommand paginated, 
            string userId, string role, CancellationToken cancellationToken)
        {
            var typeId = await GetUserTypeId(userId, role);

            var whereQueryable = role == "Artisan"
                ? GetBaseQuery()
                    .Where(x => x.Order.ArtisanId == typeId && x.IsVerified)
                    .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search))
                : GetBaseQuery().Where(x => x.Order.CustomerId == typeId && x.IsVerified)
                    .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));

            return await whereQueryable.BuildPage(paginated, cancellationToken);
        }

        public async Task<Payment> GetByTrxRef(string trxRef) 
            => await GetBaseQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.TransactionReference == trxRef);

        public async Task<int> GetArtisanPaymentCount(string userId)
        {
            return await GetBaseQuery()
                .Where(x => x.Order.Artisan.UserId == userId &&
                            !x.IsForwarded &&
                            x.IsVerified)
                .CountAsync();
        }
        
        public override IQueryable<Payment> GetBaseQuery() =>
            base.GetBaseQuery()
                .Include(x => x.Order)
                .ThenInclude(x => x.Artisan);

        protected override Expression<Func<Payment, bool>> GetSearchCondition(string search)
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