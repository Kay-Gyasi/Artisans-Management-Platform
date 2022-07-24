using AMP.Domain.Entities;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    public class PaymentRepository : RepositoryBase<Payments>, IPaymentRepository
    {
        protected PaymentRepository(AmpDbContext context, ILogger<Payments> logger) : base(context, logger)
        {
        }
    }
}