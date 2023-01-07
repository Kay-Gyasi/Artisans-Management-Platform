using AMP.Domain.Entities.BusinessManagement;
using AMP.Processors.Commands.BusinessManagement;
using AMP.Processors.Dtos.BusinessManagement;
using AMP.Processors.PageDtos.BusinessManagement;

namespace AMP.Processors.Processors.BusinessManagement
{
    [Processor]
    public class DisputeProcessor : ProcessorBase
    {
        private const string LookupCacheKey = "Disputelookup";

        public DisputeProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache) : base(uow, mapper, cache)
        {
        }

        public async Task<Result<string>> Save(DisputeCommand command, string userId)
        {
            var isNew = string.IsNullOrEmpty(command.Id);
            command.CustomerId = await Uow.Customers.GetCustomerId(userId);

            Dispute dispute;
            if (isNew)
            {
                dispute = Dispute.Create(command.CustomerId, command.OrderId)
                    .CreatedOn(DateTime.UtcNow);
                AssignFields(dispute, command, true);
                Cache.Remove(LookupCacheKey);
                await Uow.Disputes.InsertAsync(dispute);
                await Uow.SaveChangesAsync();
                return new Result<string>(dispute.Id);
            }

            dispute = await Uow.Disputes.GetAsync(command.Id);
            if (dispute is null)
                return new Result<string>(new InvalidIdException($"Dispute with id: {command.Id} does not exist"));
            AssignFields(dispute, command);
            Cache.Remove(LookupCacheKey);
            await Uow.Disputes.UpdateAsync(dispute);
            await Uow.SaveChangesAsync();
            return new Result<string>(dispute.Id);
        }

        public async Task<PaginatedList<DisputePageDto>> GetPage(PaginatedCommand command, string userId)
        {
            var page = await Uow.Disputes.GetUserPage(command, userId, new CancellationToken());
            return Mapper.Map<PaginatedList<DisputePageDto>>(page);
        }

        public async Task<Result<DisputeDto>> Get(string id)
        {
            var dispute = Mapper.Map<DisputeDto>(await Uow.Disputes.GetAsync(id));
            return dispute is null ?
                new Result<DisputeDto>(new InvalidIdException($"Dispute with id: {id} does not exist")) 
                : new Result<DisputeDto>(dispute);
        }
        
        public async Task<Result<DisputeCount>> GetOpenDisputeCount(string userId)
        {
            try
            {
                return new Result<DisputeCount>(new DisputeCount
                {
                    Count = await Uow.Disputes.OpenDisputeCount(userId)
                });
            }
            catch (Exception e)
            {
                return new Result<DisputeCount>(e);
            }
        }

        public async Task<Result<bool>> Delete(string id)
        {
            try
            {
                await Uow.Disputes.DeleteAsync(id);
                Cache.Remove(LookupCacheKey);
                return new Result<bool>(true);
            }
            catch (Exception e)
            {
                return new Result<bool>(e);
            }
        }

        private static void AssignFields(Dispute dispute, DisputeCommand command, bool isNew = false)
        {
            dispute.WithDetails(command.Details)
                .WithStatus(command.Status);

            if (!isNew)
                dispute.ByCustomerWithId(command.CustomerId)
                    .AgainstOrderWithId(command.OrderId);
        }
    }
}