using AMP.Domain.Entities.BusinessManagement;
using AMP.Processors.Repositories.BusinessManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AMP.Processors.Processors.BusinessManagement;

[Processor]
public class PaymentWithdrawalProcessor : Processor
{
    private readonly IPaymentWithdrawalRepository _paymentWithdrawalRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly ILogger<PaymentWithdrawalProcessor> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private string UserId => _httpContextAccessor.HttpContext
        .User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public PaymentWithdrawalProcessor(IUnitOfWork uow, 
        IMapper mapper, 
        IMemoryCache cache,
        IPaymentWithdrawalRepository paymentWithdrawalRepository,
        IPaymentRepository paymentRepository,
        ILogger<PaymentWithdrawalProcessor> logger,
        IHttpContextAccessor httpContextAccessor) 
        : base(uow, mapper, cache)
    {
        _paymentWithdrawalRepository = paymentWithdrawalRepository;
        _paymentRepository = paymentRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<bool>> SaveArtisanWithdrawalAsync()
    {
        try
        {
            // TODO:: Send sms to admins
            var details = await _paymentRepository.GetWithdrawalDetails(UserId);
            var withdrawal = PaymentWithdrawal.Create(UserId, details.Item1)
                .SendByMomo(MomoTransfer.Create(details.Item2));
            await Uow.PaymentWithdrawals.InsertAsync(withdrawal);
            await Uow.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
            return new Result<bool>(e);
        }
    }
}