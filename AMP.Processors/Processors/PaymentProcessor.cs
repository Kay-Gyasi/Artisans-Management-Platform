using AMP.Domain.Entities;
using AMP.Processors.Commands;
using AMP.Processors.Dtos;
using AMP.Processors.PageDtos;
using AMP.Processors.Processors.Base;
using AMP.Shared.Domain.Models;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Interfaces.UoW;

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
            Cache.Remove(LookupCacheKey);
            await Uow.Payments.InsertAsync(payment);
            await Uow.SaveChangesAsync();
            return payment.Id;
        }

        public async Task Verify(VerifyPaymentCommand command)
        {
            await Uow.Payments.Verify(command.Reference, command.TransactionReference);
            await Uow.SaveChangesAsync();
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