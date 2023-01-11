using AMP.Domain.Entities.BusinessManagement;
using AMP.Processors.Commands.BusinessManagement;
using AMP.Processors.Dtos.BusinessManagement;
using AMP.Processors.PageDtos.BusinessManagement;
using AMP.Processors.Responses;
using AMP.Processors.Workers.BackgroundWorker;

namespace AMP.Processors.Processors.BusinessManagement
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

        public async Task<int> GetScheduleCount(string userId)
        {
            return await Uow.Orders.GetScheduleCount(userId);
        }
        
        public async Task<int> GetJobRequestsCount(string userId)
        {
            return await Uow.Orders.GetJobRequestsCount(userId);
        }

        public async Task<Result<InsertOrderResponse>> Insert(OrderCommand command)
        {
            var order = Order.Create(command.CustomerId, command.ServiceId)
                .WithReferenceNo(await Task.Run(() => RandomStringHelper.Generate(12)));
            await AssignFields(order, command, true);
            Cache.Remove(LookupCacheKey);
            await Uow.Orders.InsertAsync(order);
            await Uow.SaveChangesAsync();

            var service = await Uow.Services.GetNameAsync(order.ServiceId);
            return string.IsNullOrEmpty(service) 
                ? new Result<InsertOrderResponse>(new InvalidIdException($"Service with id: {order.ServiceId} does not exist"))
                : new Result<InsertOrderResponse>(new InsertOrderResponse { OrderId = order.Id, Service = service });
        }

        public async Task<Result<bool>> SetCost(SetCostCommand costCommand)
        {
            try
            {
                await Uow.Orders.SetCost(costCommand);
            }
            catch (Exception e)
            {
                return new Result<bool>(e);
            }
            return new Result<bool>(true);
        }

        public async Task<Result<string>> Save(OrderCommand command)
        {
            var order = await Uow.Orders.GetAsync(command.Id);
            if (order is null)
                return new Result<string>(
                    new InvalidIdException($"Order with id: {command.Id} does not exist"));
            await AssignFields(order, command);
            Cache.Remove(LookupCacheKey);
            await Uow.Orders.UpdateAsync(order);
            await Uow.SaveChangesAsync();
            return new Result<string>(order.Id);
        }

        public async Task<Result<bool>> UnassignArtisan(string orderId)
        {
            try
            {
                var userId = await Uow.Orders.UnassignArtisan(orderId);
                _worker.ServeHub(DataCountType.Schedule, userId);
                _worker.ServeHub(DataCountType.JobRequests, userId);
            }
            catch (Exception e)
            {
                return new Result<bool>(e);
            }

            return new Result<bool>(true);
        }

        public async Task<Result<bool>> AssignArtisan(string orderId, string artisanId)
        {
            try
            {
                var artisanUserId = await Uow.Orders.AssignArtisan(orderId, artisanId);
                if (await Uow.SaveChangesAsync())
                {
                    _worker.SendSms(SmsType.AssignArtisan, orderId, artisanId);
                    _worker.ServeHub(DataCountType.JobRequests, artisanUserId);
                }
            }
            catch (Exception e)
            {
                return new Result<bool>(e);
            }

            return new Result<bool>(true);
        }

        public async Task<Result<bool>> AcceptRequest(string orderId)
        {
            try
            {
                var artisanUserId = await Uow.Orders.AcceptRequest(orderId);
                _worker.SendSms(SmsType.AcceptRequest, orderId);
                _worker.ServeHub(DataCountType.Schedule, artisanUserId);
                _worker.ServeHub(DataCountType.JobRequests, artisanUserId);
            }
            catch (Exception e)
            {
                return new Result<bool>(e);
            }

            return new Result<bool>(true);
        }

        public async Task<Result<bool>> CancelRequest(string orderId)
        {
            try
            {
                var artisanUserId = await Uow.Orders.CancelRequest(orderId);
                _worker.ServeHub(DataCountType.Schedule, artisanUserId);
                _worker.ServeHub(DataCountType.JobRequests, artisanUserId);
            }
            catch (Exception e)
            {
                return new Result<bool>(e);
            }

            return new Result<bool>(true);
        }

        public async Task<Result<bool>> Complete(string orderId)
        {
            try
            {
                await Uow.Orders.Complete(orderId);
            }
            catch (Exception e)
            {
                return new Result<bool>(e);
            }

            return new Result<bool>(true);
        }
        
        public async Task<Result<bool>> ArtisanComplete(string orderId)
        {
            try
            {
                var artisanUserId = await Uow.Orders.ArtisanComplete(orderId);
                _worker.ServeHub(DataCountType.Schedule, artisanUserId);
            }
            catch (Exception e)
            {
                return new Result<bool>(e);
            }

            return new Result<bool>(true);
        }

        public async Task<PaginatedList<OrderPageDto>> GetPage(PaginatedCommand command)
        {
            var page = await Uow.Orders.GetPage(command, new CancellationToken());
            return Mapper.Map<PaginatedList<OrderPageDto>>(page);
        }

        public async Task<Result<OrderDto>> Get(string id)
        {
            var order = Mapper.Map<OrderDto>(await Uow.Orders.GetAsync(id));
            if (order is null)
                return new Result<OrderDto>(new InvalidIdException($"Order with id: {id} does not exist"));
            order.PaymentMade = await Uow.Payments.AmountPaid(id);
            return new Result<OrderDto>(order);
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

        public async Task<Result<bool>> Delete(string id)
        {
            try
            {
                await Uow.Orders.DeleteAsync(id);
                Cache.Remove(LookupCacheKey);
            }
            catch (Exception e)
            {
                return new Result<bool>(e);
            }

            return new Result<bool>(true);
        }

        private async Task AssignFields(Order order, OrderCommand command, bool isNew = false)
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

            if (!isNew) order.ForServiceWithId(command.ServiceId);
        }
    }
}