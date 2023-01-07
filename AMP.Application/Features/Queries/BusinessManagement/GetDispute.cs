using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Dtos;
using AMP.Processors.Dtos.BusinessManagement;
using AMP.Processors.Processors;
using AMP.Processors.Processors.BusinessManagement;
using MediatR;

namespace AMP.Application.Features.Queries
{
    public class GetDispute
    {
        public class Query : IRequest<Result<DisputeDto>>
        {
            public string Id { get; }

            public Query(string id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Query, Result<DisputeDto>>
        {
            private readonly DisputeProcessor _processor;

            public Handler(DisputeProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Result<DisputeDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.Get(request.Id);
            }
        }
    }
}