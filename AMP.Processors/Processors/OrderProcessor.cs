using AMP.Domain.Entities;
using AMP.Domain.ValueObjects;
using AMP.Processors.Commands;
using AMP.Processors.Dtos;
using AMP.Processors.PageDtos;
using AMP.Processors.Processors.Base;
using AMP.Processors.Repositories.UoW;
using AMP.Processors.Responses;
using AMP.Shared.Domain.Models;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AMP.Processors.Processors
{
    [Processor]
    public class OrderProcessor : ProcessorBase
    {
        private const string LookupCacheKey = "Orderlookup";

        public OrderProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache) : base(uow, mapper, cache)
        {
        }

        public async Task<InsertOrderResponse> Insert(OrderCommand command)
        {
            var order = Orders.Create(command.CustomerId, command.ServiceId)
                .CreatedOn(DateTime.UtcNow);
            await AssignFields(order, command, true);
            _cache.Remove(LookupCacheKey);
            await _uow.Orders.InsertAsync(order);
            await _uow.SaveChangesAsync();

            var service = await _uow.Services.GetNameAsync(order.ServiceId);
            return new InsertOrderResponse { OrderId = order.Id, Service = service };
        }

        public async Task SetCost(SetCostCommand costCommand)
        {
            await _uow.Orders.SetCost(costCommand);
            await _uow.SaveChangesAsync();
        }

        public async Task<int> Save(OrderCommand command)
        {
            var order = await _uow.Orders.GetAsync(command.Id);
            await AssignFields(order, command);
            _cache.Remove(LookupCacheKey);
            await _uow.Orders.UpdateAsync(order);
            await _uow.SaveChangesAsync();
            return order.Id;
        }

        public async Task UnassignArtisan(int orderId)
        {
            await _uow.Orders.UnassignArtisan(orderId);
            await _uow.SaveChangesAsync();
        }

        public async Task AssignArtisan(int orderId, int artisanId)
        {
            await _uow.Orders.AssignArtisan(orderId, artisanId);
            await _uow.SaveChangesAsync();
        }

        public async Task AcceptRequest(int orderId)
        {
            await _uow.Orders.AcceptRequest(orderId);
            await _uow.SaveChangesAsync();
        }

        public async Task CancelRequest(int orderId)
        {
            await _uow.Orders.CancelRequest(orderId);
            await _uow.SaveChangesAsync();
        }

        public async Task Complete(int orderId)
        {
            await _uow.Orders.Complete(orderId);
            await _uow.SaveChangesAsync();
        }
        
        public async Task ArtisanComplete(int orderId)
        {
            await _uow.Orders.ArtisanComplete(orderId);
            await _uow.SaveChangesAsync();
        }

        public async Task<PaginatedList<OrderPageDto>> GetPage(PaginatedCommand command)
        {
            var page = await _uow.Orders.GetPage(command, new CancellationToken());
            return _mapper.Map<PaginatedList<OrderPageDto>>(page);
        }

        public async Task<OrderDto> Get(int id)
        {
            var order = _mapper.Map<OrderDto>(await _uow.Orders.GetAsync(id));
            order.PaymentMade = await _uow.Payments.AmountPaid(id);
            return order;
        }

        public async Task<PaginatedList<OrderPageDto>> GetSchedule(PaginatedCommand command, int userId)
        {
            var page = await _uow.Orders.GetSchedule(command, userId, new CancellationToken());
            return _mapper.Map<PaginatedList<OrderPageDto>>(page);
        }

        public async Task<PaginatedList<OrderPageDto>> GetRequests(PaginatedCommand command, int userId)
        {
            var page = await _uow.Orders.GetRequests(command, userId, new CancellationToken());
            return _mapper.Map<PaginatedList<OrderPageDto>>(page);
        }

        public async Task<PaginatedList<OrderPageDto>> GetWorkHistory(PaginatedCommand command, int userId)
        {
            var page = await _uow.Orders.GetWorkHistory(command, userId, new CancellationToken());
            return _mapper.Map<PaginatedList<OrderPageDto>>(page);
        }

        public async Task<PaginatedList<OrderPageDto>> GetOrderHistory(PaginatedCommand command, int userId)
        {
            var page = await _uow.Orders.GetOrderHistory(command, userId, new CancellationToken());
            return _mapper.Map<PaginatedList<OrderPageDto>>(page);
        }

        public async Task<PaginatedList<OrderPageDto>> GetCustomerOrderPage(PaginatedCommand command, int userId)
        {
            var page = await _uow.Orders.GetCustomerOrderPage(command, userId, new CancellationToken());
            return _mapper.Map<PaginatedList<OrderPageDto>>(page);
        }

        public async Task Delete(int id)
        {
            var order = await _uow.Orders.GetAsync(id);
            _cache.Remove(LookupCacheKey);
            if (order != null) await _uow.Orders.SoftDeleteAsync(order);
            await _uow.SaveChangesAsync();
        }

        private async Task AssignFields(Orders order, OrderCommand command, bool isNew = false)
        {
            var customerId = await _uow.Customers.GetCustomerId(command.UserId);
            order.ForArtisanWithId(command.ArtisanId)
                //.IsCompleted(command.IsComplete)
                .WithDescription(command.Description)
                .WithCost(command.Cost)
                .WithUrgency(command.Urgency)
                .WithStatus(command.Status)
                .WithPreferredStartDate(command.PreferredStartDate)
                .WithPreferredCompletionDate(command.PreferredCompletionDate)
                .WithWorkAddress(_mapper.Map<Address>(command.WorkAddress))
                .ForCustomerWithId(customerId)
                .WithScope(command.Scope);

            if (!isNew) order.ForServiceWithId(command.ServiceId);
        }
    }
}