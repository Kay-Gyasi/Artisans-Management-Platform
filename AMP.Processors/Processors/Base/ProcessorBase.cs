using AMP.Processors.Interfaces.UoW;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace AMP.Processors.Processors.Base
{
    public class ProcessorBase
    {
        protected readonly IUnitOfWork Uow;
        protected readonly IMapper Mapper;
        protected readonly IMemoryCache Cache;

        protected ProcessorBase(IUnitOfWork uow, IMapper mapper, IMemoryCache cache)
        {
            Uow = uow;
            Mapper = mapper;
            Cache = cache;
        }
    }
}