using System.Linq;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    [Repository]
    public class PaymentRepository : RepositoryBase<Payments>, IPaymentRepository
    {
        public PaymentRepository(AmpDbContext context, ILogger<Payments> logger) : base(context, logger)
        {
        }

        public async Task Verify(string reference, string trxRef)
        {
            var payment = await GetBaseQuery().FirstOrDefaultAsync(x => x.Reference == reference);
            payment.HasBeenVerified(true);
            payment.WithTransactionReference(trxRef);
        }

        public override IQueryable<Payments> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.Order);
        }
    }
}