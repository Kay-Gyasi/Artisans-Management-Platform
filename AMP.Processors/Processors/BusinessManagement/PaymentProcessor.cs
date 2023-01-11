using AMP.Domain.Entities.BusinessManagement;
using AMP.Processors.Commands.BusinessManagement;
using AMP.Processors.Dtos.BusinessManagement;
using AMP.Processors.PageDtos.BusinessManagement;
using AMP.Processors.Workers.BackgroundWorker;

namespace AMP.Processors.Processors.BusinessManagement
{
    [Processor]
    public class PaymentProcessor : ProcessorBase
    {
        private readonly IBackgroundWorker _worker;
        private const string LookupCacheKey = "Paymentlookup";

        public PaymentProcessor(IUnitOfWork uow, IMapper mapper, 
            IMemoryCache cache,
            IBackgroundWorker worker) : base(uow, mapper, cache)
        {
            _worker = worker;
        }
        
        public async Task<int> GetArtisanPaymentsCount(string userId)
        {
            return await Uow.Payments.GetArtisanPaymentCount(userId);
        }

        public async Task<string> Save(PaymentCommand command)
        {

            var payment = Payment.Create(command.OrderId);
            AssignField(payment, command, true);
            Cache.Remove(LookupCacheKey);
            await Uow.Payments.InsertAsync(payment);
            await Uow.SaveChangesAsync();
            return payment.Id;
        }

        public async Task Verify(VerifyPaymentCommand command)
        {
            var artisanUserId = await Uow.Payments.Verify(command.Reference, command.TransactionReference);
            var success = await Uow.SaveChangesAsync();
            if (success)
            {
                _worker.SendSms(SmsType.PaymentVerified, command.TransactionReference);
                _worker.ServeHub(DataCountType.Payments, artisanUserId);
            }
        }

        public async Task<PaginatedList<PaymentPageDto>> GetPage(PaginatedCommand command, string userId, string role)
        {
            var page = await Uow.Payments.GetUserPage(command, userId, role, new CancellationToken());
            return Mapper.Map<PaginatedList<PaymentPageDto>>(page);
        }

        public async Task<PaymentDto> Get(string id)
        {
            return Mapper.Map<PaymentDto>(await Uow.Payments.GetAsync(id));
        }

        public async Task Delete(string id)
        {
            var payment = await Uow.Payments.GetAsync(id);
            Cache.Remove(LookupCacheKey);
            if (payment != null) await Uow.Payments.SoftDeleteAsync(payment);
            await Uow.SaveChangesAsync();
        }

        private static void AssignField(Payment payment, PaymentCommand command, bool isNew = false)
        {
            payment.WithAmountPaid(command.AmountPaid)
                .HasBeenForwarded(false)
                .WithReference(command.Reference)
                .HasBeenVerified(false);

            if (!isNew)
                payment.OnOrderWithId(command.OrderId);
        }
    }
}