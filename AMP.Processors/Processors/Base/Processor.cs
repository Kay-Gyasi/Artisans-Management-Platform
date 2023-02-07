namespace AMP.Processors.Processors.Base
{
    public class Processor
    {
        protected readonly IUnitOfWork Uow;
        protected readonly IMapper Mapper;
        protected readonly IMemoryCache Cache;

        protected Processor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache)
        {
            Uow = uow;
            Mapper = mapper;
            Cache = cache;
        }
    }
}