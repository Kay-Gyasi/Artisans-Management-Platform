using AMP.Processors.Repositories.UoW;
using AutoMapper;

namespace AMP.Processors.Processors.Base
{
    public class ProcessorBase
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IMapper _mapper;

        public ProcessorBase(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
    }
}