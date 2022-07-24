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
    public class OrderProcessor : ProcessorBase
    {
        public OrderProcessor(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }

        public async Task<int> Save(OrderCommand command)
        {
            var isNew = command.Id == 0;

            Orders order;
            if (isNew)
            {
                order = Orders.Create(command.CustomerId, command.ServiceId)
                    .CreatedOn(DateTime.UtcNow);  
                AssignFields(order, command, true);
                await _uow.Orders.InsertAsync(order);
                await _uow.SaveChangesAsync();
                return order.Id;
            }

            order = await _uow.Orders.GetAsync(command.Id);
            AssignFields(order, command);
            await _uow.Orders.UpdateAsync(order);
            await _uow.SaveChangesAsync();
            return order.Id;
        }

        public async Task<PaginatedList<OrderPageDto>> GetPage(PaginatedCommand command)
        {
            var page = await _uow.Orders.GetPage(command, new CancellationToken());
            return _mapper.Map<PaginatedList<OrderPageDto>>(page);
        }

        public async Task<OrderDto> Get(int id)
        {
            return _mapper.Map<OrderDto>(await _uow.Orders.GetAsync(id));
        }

        public async Task Delete(int id)
        {
            var artisan = await _uow.Orders.GetAsync(id);
            if (artisan != null) await _uow.Orders.DeleteAsync(artisan, new CancellationToken());
            await _uow.SaveChangesAsync();
        }

        private void AssignFields(Orders order, OrderCommand command, bool isNew = false)
        {
            order.ForArtisanWithId(command.ArtisanId)
                .IsCompleted(command.IsComplete)
                .WithPaymentId(command.PaymentId)
                .WithDescription(command.Description)
                .WithCost(command.Cost)
                .WithUrgency(command.Urgency)
                .WithStatus(command.Status)
                .WithPreferredDate(command.PreferredDate);

            if (!isNew)
                order.ForCustomerWithId(command.CustomerId)
                    .ForServiceWithId(command.ServiceId);
        }
    }
}