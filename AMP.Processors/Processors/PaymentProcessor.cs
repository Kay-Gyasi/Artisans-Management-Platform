using System;
using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Commands;
using AMP.Processors.Dtos;
using AMP.Processors.PageDtos;
using AMP.Processors.Payment;
using AMP.Processors.Processors.Base;
using AMP.Processors.Repositories.UoW;
using AMP.Shared.Domain.Models;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace AMP.Processors.Processors
{
    [Processor]
    public class PaymentProcessor : ProcessorBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache,
            IPaymentService paymentService) : base(uow, mapper, cache)
        {
            _paymentService = paymentService;
        }

        public async Task<int> Save(PaymentCommand command, int userId)
        {
            var customer = await _uow.Customers.GetByUserIdAsync(userId);

            var momoCommand = new MobileMoneyPayCommand(customer, command);
            await _paymentService.PayViaMobileMoney(momoCommand);

            var payment = Payments.Create(customer.Id, command.OrderId)
                .CreatedOn(DateTime.UtcNow);
            AssignField(payment, command, customer.Id, true);
            await _uow.Payments.InsertAsync(payment);
            await _uow.SaveChangesAsync();
            return payment.Id;
        }

        public async Task<PaginatedList<PaymentPageDto>> GetPage(PaginatedCommand command)
        {
            var page = await _uow.Payments.GetPage(command, new CancellationToken());
            return _mapper.Map<PaginatedList<PaymentPageDto>>(page);
        }

        public async Task<PaymentDto> Get(int id)
        {
            return _mapper.Map<PaymentDto>(await _uow.Payments.GetAsync(id));
        }

        public async Task Delete(int id)
        {
            var artisan = await _uow.Payments.GetAsync(id);
            if (artisan != null) await _uow.Payments.DeleteAsync(artisan, new CancellationToken());
            await _uow.SaveChangesAsync();
        }

        private void AssignField(Payments payment, PaymentCommand command, int customerId, bool isNew = false)
        {
            payment.WithAmountPaid(command.AmountPaid)
                .WithStatus(command.Status);

            if (!isNew)
                payment.ByCustomerWithId(customerId)
                    .OnOrderWithId(command.OrderId);
        }
    }
}