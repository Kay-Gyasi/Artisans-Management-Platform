using AMP.Domain.Entities;
using AMP.Processors.Commands;
using AMP.Processors.Dtos;
using AMP.Processors.PageDtos;
using AMP.Processors.Processors.Base;
using AMP.Processors.Repositories.UoW;
using AMP.Shared.Domain.Models;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AMP.Processors.Processors
{
    [Processor]
    public class PaymentProcessor : ProcessorBase
    {
        private const string LookupCacheKey = "Paymentlookup";

        public PaymentProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache) : base(uow, mapper, cache)
        {
        }

        public async Task<string> Save(PaymentCommand command)
        {

            var payment = Payments.Create(command.OrderId)
                .CreatedOn(DateTime.UtcNow);
            AssignField(payment, command, true);
            _cache.Remove(LookupCacheKey);
            await _uow.Payments.InsertAsync(payment);
            await _uow.SaveChangesAsync();
            return payment.Id;
        }

        public async Task Verify(VerifyPaymentCommand command)
        {
            await _uow.Payments.Verify(command.Reference, command.TransactionReference);
            await _uow.SaveChangesAsync();
        }

        public async Task<PaginatedList<PaymentPageDto>> GetPage(PaginatedCommand command)
        {
            var page = await _uow.Payments.GetPage(command, new CancellationToken());
            return _mapper.Map<PaginatedList<PaymentPageDto>>(page);
        }

        public async Task<PaymentDto> Get(string id)
        {
            return _mapper.Map<PaymentDto>(await _uow.Payments.GetAsync(id));
        }

        public async Task Delete(string id)
        {
            var payment = await _uow.Payments.GetAsync(id);
            _cache.Remove(LookupCacheKey);
            if (payment != null) await _uow.Payments.SoftDeleteAsync(payment);
            await _uow.SaveChangesAsync();
        }

        private static void AssignField(Payments payment, PaymentCommand command, bool isNew = false)
        {
            payment.WithAmountPaid(command.AmountPaid)
                .HasBeenForwarded(false)
                .WithReference(command.Reference)
                .HasBeenVerified(false);

            if (!isNew)
                payment.OnOrderWithId(command.OrderId)
                    .LastModifiedOn();
        }
    }
}