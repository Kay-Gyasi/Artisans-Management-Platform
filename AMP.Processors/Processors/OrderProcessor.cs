using AMP.Domain.Entities;
using AMP.Domain.ValueObjects;
using AMP.Processors.Commands;
using AMP.Processors.Dtos;
using AMP.Processors.PageDtos;
using AMP.Processors.Processors.Base;
using AMP.Processors.Responses;
using AMP.Shared.Domain.Models;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Interfaces.UoW;

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
                .CreatedOn(DateTime.UtcNow)
                .WithReferenceNo(await Task.Run(() => GenerateReferenceNo(12)));
            await AssignFields(order, command, true);
            Cache.Remove(LookupCacheKey);
            await Uow.Orders.InsertAsync(order);
            await Uow.SaveChangesAsync();

            var service = await Uow.Services.GetNameAsync(order.ServiceId);
            return new InsertOrderResponse { OrderId = order.Id, Service = service };
        }

        public async Task SetCost(SetCostCommand costCommand)
        {
            await Uow.Orders.SetCost(costCommand);
            await Uow.SaveChangesAsync();
        }

        public async Task<string> Save(OrderCommand command)
        {
            var order = await Uow.Orders.GetAsync(command.Id);
            await AssignFields(order, command);
            Cache.Remove(LookupCacheKey);
            await Uow.Orders.UpdateAsync(order);
            await Uow.SaveChangesAsync();
            return order.Id;
        }

        public async Task UnassignArtisan(string orderId)
        {
            await Uow.Orders.UnassignArtisan(orderId);
            await Uow.SaveChangesAsync();
        }

        public async Task AssignArtisan(string orderId, string artisanId)
        {
            await Uow.Orders.AssignArtisan(orderId, artisanId);
            await Uow.SaveChangesAsync();
        }

        public async Task AcceptRequest(string orderId)
        {
            await Uow.Orders.AcceptRequest(orderId);
            await Uow.SaveChangesAsync();
        }

        public async Task CancelRequest(string orderId)
        {
            await Uow.Orders.CancelRequest(orderId);
            await Uow.SaveChangesAsync();
        }

        public async Task Complete(string orderId)
        {
            await Uow.Orders.Complete(orderId);
            await Uow.SaveChangesAsync();
        }
        
        public async Task ArtisanComplete(string orderId)
        {
            await Uow.Orders.ArtisanComplete(orderId);
            await Uow.SaveChangesAsync();
        }

        public async Task<PaginatedList<OrderPageDto>> GetPage(PaginatedCommand command)
        {
            var page = await Uow.Orders.GetPage(command, new CancellationToken());
            return Mapper.Map<PaginatedList<OrderPageDto>>(page);
        }

        public async Task<OrderDto> Get(string id)
        {
            var order = Mapper.Map<OrderDto>(await Uow.Orders.GetAsync(id));
            order.PaymentMade = await Uow.Payments.AmountPaid(id);
            return order;
        }

        public async Task<PaginatedList<OrderPageDto>> GetSchedule(PaginatedCommand command, string userId)
        {
            var page = await Uow.Orders.GetSchedule(command, userId, new CancellationToken());
            return Mapper.Map<PaginatedList<OrderPageDto>>(page);
        }

        public async Task<PaginatedList<OrderPageDto>> GetRequests(PaginatedCommand command, string userId)
        {
            var page = await Uow.Orders.GetRequests(command, userId, new CancellationToken());
            return Mapper.Map<PaginatedList<OrderPageDto>>(page);
        }

        public async Task<PaginatedList<OrderPageDto>> GetWorkHistory(PaginatedCommand command, string userId)
        {
            var page = await Uow.Orders.GetWorkHistory(command, userId, new CancellationToken());
            return Mapper.Map<PaginatedList<OrderPageDto>>(page);
        }

        public async Task<PaginatedList<OrderPageDto>> GetOrderHistory(PaginatedCommand command, string userId)
        {
            var page = await Uow.Orders.GetOrderHistory(command, userId, new CancellationToken());
            return Mapper.Map<PaginatedList<OrderPageDto>>(page);
        }

        public async Task<PaginatedList<OrderPageDto>> GetCustomerOrderPage(PaginatedCommand command, string userId)
        {
            var page = await Uow.Orders.GetCustomerOrderPage(command, userId, new CancellationToken());
            return Mapper.Map<PaginatedList<OrderPageDto>>(page);
        }

        public async Task Delete(string id)
        {
            var order = await Uow.Orders.GetAsync(id);
            Cache.Remove(LookupCacheKey);
            if (order != null) await Uow.Orders.SoftDeleteAsync(order);
            await Uow.SaveChangesAsync();
        }

        private async Task AssignFields(Orders order, OrderCommand command, bool isNew = false)
        {
            var customerId = await Uow.Customers.GetCustomerId(command.UserId);
            order.ForArtisanWithId(command.ArtisanId)
                //.IsCompleted(command.IsComplete)
                .WithDescription(command.Description)
                .WithCost(command.Cost)
                .WithUrgency(command.Urgency)
                .WithStatus(command.Status)
                .WithPreferredStartDate(command.PreferredStartDate)
                .WithPreferredCompletionDate(command.PreferredCompletionDate)
                .WithWorkAddress(Mapper.Map<Address>(command.WorkAddress))
                .ForCustomerWithId(customerId)
                .WithScope(command.Scope);

            if (!isNew) order.ForServiceWithId(command.ServiceId)
                    .LastModifiedOn();
        }

        // Generates a random string with a given size.    
        private static string GenerateReferenceNo(int size, bool lowerCase = false)
        {
            var rand = new Random();
            var builder = new StringBuilder(size);  
  
            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            var offset = lowerCase ? 'a' : 'A';  
            const int lettersOffset = 26; // A...Z or a..z: length=26  
  
            for (var i = 0; i < size; i++)  
            {  
                var @char = (char)rand.Next(offset, offset + lettersOffset);  
                builder.Append(@char);  
            }  
  
            return lowerCase ? builder.ToString().ToLower() : builder.ToString();  
        }  
    }
}