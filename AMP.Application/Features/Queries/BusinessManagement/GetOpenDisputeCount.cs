using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Commands;
using AMP.Processors.Commands.BusinessManagement;
using AMP.Processors.Processors;
using AMP.Processors.Processors.BusinessManagement;
using MediatR;

namespace AMP.Application.Features.Queries
{
    public class GetOpenDisputeCount
    {
        public class Query : IRequest<Result<DisputeCount>>
        {
            public string UserId { get; }

            public Query(string userId)
            {
                UserId = userId;
            }
        }

        public class Handler : IRequestHandler<Query, Result<DisputeCount>>
        {
            private readonly DisputeProcessor _processor;

            public Handler(DisputeProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Result<DisputeCount>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetOpenDisputeCount(request.UserId);
            }
        }
    }
}