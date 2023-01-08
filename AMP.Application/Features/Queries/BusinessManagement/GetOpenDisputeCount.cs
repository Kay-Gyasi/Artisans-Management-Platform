using AMP.Processors.Commands.BusinessManagement;
using AMP.Processors.Processors.BusinessManagement;

namespace AMP.Application.Features.Queries.BusinessManagement
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