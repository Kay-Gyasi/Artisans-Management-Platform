using AMP.Processors.Exceptions;
using AMP.Processors.Responses;
using AMP.Processors.Workers;
using AMP.Processors.Workers.BackgroundWorker;
using AMP.Processors.Workers.Enums;

namespace AMP.Processors.Processors
{
    [Processor]
    public class OrderProcessor : ProcessorBase
    {
        private readonly IBackgroundWorker _worker;
        private const string LookupCacheKey = "Orderlookup";

        public OrderProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache,
            IBackgroundWorker worker) : base(uow, mapper, cache)
        {
            _worker = worker;
        }

        public async Task<InsertOrderResponse> Insert(OrderCommand command)
        {
            var order = Orders.Create(command.CustomerId, command.ServiceId)
                .CreatedOn(DateTime.UtcNow)
                .WithReferenceNo(await Task.Run(() => RandomStringHelper.Generate(12)));
            await AssignFields(order, command, true);
            Cache.Remove(LookupCacheKey);
            await Uow.Orders.InsertAsync(order);
            await Uow.SaveChangesAsync();

            var service = await Uow.Services.GetNameAsync(order.ServiceId);
            return string.IsNullOrEmpty(service) 
                ? throw new InvalidIdException($"Service with id: {order.ServiceId} does not exist")
                : new InsertOrderResponse { OrderId = order.Id, Service = service };
        }

        public Task SetCost(SetCostCommand costCommand)
        {
            return Uow.Orders.SetCost(costCommand);
        }

        public async Task<string> Save(OrderCommand command)
        {
            var order = await Uow.Orders.GetAsync(command.Id);
            if (order is null) throw new InvalidIdException($"Order with id: {command.Id} does not exist");
            await AssignFields(order, command);
            Cache.Remove(LookupCacheKey);
            await Uow.Orders.UpdateAsync(order);
            await Uow.SaveChangesAsync();
            return order.Id;
        }

        public Task UnassignArtisan(string orderId)
        {
            return Uow.Orders.UnassignArtisan(orderId);
        }

        public async Task AssignArtisan(string orderId, string artisanId)
        {
            await Uow.Orders.AssignArtisan(orderId, artisanId);
            var success = await Uow.SaveChangesAsync();
            if(success) _worker.SendSms(SmsType.AssignArtisan, orderId, artisanId);
        }

        public async Task AcceptRequest(string orderId)
        {
            await Uow.Orders.AcceptRequest(orderId);
            _worker.SendSms(SmsType.AcceptRequest, orderId);
        }

        public Task CancelRequest(string orderId)
        {
            return Uow.Orders.CancelRequest(orderId);
        }

        public Task Complete(string orderId)
        {
            return Uow.Orders.Complete(orderId);
        }
        
        public Task ArtisanComplete(string orderId)
        {
            return Uow.Orders.ArtisanComplete(orderId);
        }

        public async Task<PaginatedList<OrderPageDto>> GetPage(PaginatedCommand command)
        {
            var page = await Uow.Orders.GetPage(command, new CancellationToken());
            return Mapper.Map<PaginatedList<OrderPageDto>>(page);
        }

        public async Task<OrderDto> Get(string id)
        {
            var order = Mapper.Map<OrderDto>(await Uow.Orders.GetAsync(id));
            if (order is null) throw new InvalidIdException($"Order with id: {id} does not exist");
            order.PaymentMade = await Uow.Payments.AmountPaid(id);
            return order;
        }

        public async Task<PaginatedList<OrderPageDto>> GetSchedule(PaginatedCommand command, string userId)
        {
            return Mapper.Map<PaginatedList<OrderPageDto>>(
                await Uow.Orders.GetSchedule(command, userId, new CancellationToken()));
        }

        public async Task<PaginatedList<OrderPageDto>> GetRequests(PaginatedCommand command, string userId)
        {
            return Mapper.Map<PaginatedList<OrderPageDto>>(
                await Uow.Orders.GetRequests(command, userId, new CancellationToken()));
        }

        public async Task<PaginatedList<OrderPageDto>> GetWorkHistory(PaginatedCommand command, string userId)
        {
            return Mapper.Map<PaginatedList<OrderPageDto>>(
                await Uow.Orders.GetWorkHistory(command, userId, new CancellationToken()));
        }

        public async Task<PaginatedList<OrderPageDto>> GetOrderHistory(PaginatedCommand command, string userId)
        {
            return Mapper.Map<PaginatedList<OrderPageDto>>(
                await Uow.Orders.GetOrderHistory(command, userId, new CancellationToken()));
        }

        public async Task<PaginatedList<OrderPageDto>> GetCustomerOrderPage(PaginatedCommand command, string userId)
        {
            return Mapper.Map<PaginatedList<OrderPageDto>>(
                await Uow.Orders.GetCustomerOrderPage(command, userId, new CancellationToken()));
        }

        public async Task Delete(string id)
        {
            await Uow.Orders.DeleteAsync(id);
            Cache.Remove(LookupCacheKey);
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
                .SetLastModified();
        }
    }
}