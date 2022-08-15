using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Commands;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Queries
{
    public class GetOpenDisputeCount
    {
        public class Query : IRequest<DisputeCount>
        {
            public int UserId { get; }

            public Query(int userId)
            {
                UserId = userId;
            }
        }

        public class Handler : IRequestHandler<Query, DisputeCount>
        {
            private readonly DisputeProcessor _processor;

            public Handler(DisputeProcessor processor)
            {
                _processor = processor;
            }
            public async Task<DisputeCount> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetOpenDisputeCount(request.UserId);
            }
        }
    }
}