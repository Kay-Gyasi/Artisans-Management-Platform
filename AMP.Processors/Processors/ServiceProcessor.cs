namespace AMP.Processors.Processors
{
    [Processor]
    public class ServiceProcessor : ProcessorBase
    {
        private const string LookupCacheKey = "Servicelookup";

        public ServiceProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache) : base(uow, mapper, cache)
        {
        }

        public async Task<string> Save(ServiceCommand command)
        {
            var isNew = string.IsNullOrEmpty(command.Id);

            Services service;
            if (isNew)
            {
                service = Services.Create(command.Name, command.Description)
                    .CreatedOn();
                Cache.Remove(LookupCacheKey);
                await Uow.Services.InsertAsync(service);
                await Uow.SaveChangesAsync();
                return service.Id;
            }

            service = await Uow.Services.GetAsync(command.Id);
            service.WithDescription(command.Description)
                .WithName(command.Name);
            Cache.Remove(LookupCacheKey);
            await Uow.Services.UpdateAsync(service);
            await Uow.SaveChangesAsync();
            return service.Id;
        }

        public async Task<PaginatedList<ServicePageDto>> GetPage(PaginatedCommand command)
        {
            var page = await Uow.Services.GetPage(command, new CancellationToken());
            return Mapper.Map<PaginatedList<ServicePageDto>>(page);
        }

        public async Task<ServiceDto> Get(string id)
        {
            return Mapper.Map<ServiceDto>(await Uow.Services.GetAsync(id));
        }

        public async Task Delete(string id)
        {
            var service = await Uow.Services.GetAsync(id);
            Cache.Remove(LookupCacheKey);
            if (service != null) await Uow.Services.SoftDeleteAsync(service);
            await Uow.SaveChangesAsync();
        }
    }
}