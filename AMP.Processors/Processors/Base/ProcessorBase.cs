using AMP.Processors.Repositories.UoW;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace AMP.Processors.Processors.Base
{
    public class ProcessorBase
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProcessorBase(IUnitOfWork uow, IMapper mapper, IMemoryCache cache)
        {
            _uow = uow;
            _mapper = mapper;
            _cache = cache;
        }
    }
}