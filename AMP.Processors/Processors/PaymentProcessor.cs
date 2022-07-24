using System;
using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Commands;
using AMP.Processors.Dtos;
using AMP.Processors.PageDtos;
using AMP.Processors.Processors.Base;
using AMP.Processors.Repositories.UoW;
using AMP.Shared.Domain.Models;
using AutoMapper;

namespace AMP.Processors.Processors
{
    [Processor]
    public class PaymentProcessor : ProcessorBase
    {
        public PaymentProcessor(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }

        public async Task<int> Save(PaymentCommand command)
        {
            var isNew = command.Id == 0;

            Payments payment;
            if (isNew)
            {
                payment = Payments.Create(command.CustomerId, command.OrderId)
                    .CreatedOn(DateTime.UtcNow);
                AssignField(payment, command, true);
                await _uow.Payments.InsertAsync(payment);
                await _uow.SaveChangesAsync();
                return payment.Id;
            }

            payment = await _uow.Payments.GetAsync(command.Id);
            AssignField(payment, command);
            await _uow.Payments.UpdateAsync(payment);
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

        private void AssignField(Payments payment, PaymentCommand command, bool isNew = false)
        {
            payment.WithAmountPaid(command.AmountPaid)
                .WithStatus(command.Status);

            if (!isNew)
                payment.ByCustomerWithId(command.CustomerId)
                    .OnOrderWithId(command.OrderId);
        }
    }
}