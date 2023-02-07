using AMP.Domain.Entities.BusinessManagement;
using AMP.Processors.Repositories.BusinessManagement;

namespace AMP.Persistence.Repositories.BusinessManagement;

[Repository]
public class PaymentWithdrawalRepository : Repository<PaymentWithdrawal>, IPaymentWithdrawalRepository
{
    public PaymentWithdrawalRepository(AmpDbContext context, ILogger<PaymentWithdrawal> logger) 
        : base(context, logger)
    {
    }
}