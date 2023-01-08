using AMP.Processors.Dtos.BusinessManagement;
using AMP.Processors.Processors.BusinessManagement;

namespace AMP.Application.Features.Queries.BusinessManagement
{
    public class GetOrder
    {
        public class Query : IRequest<Result<OrderDto>>
        {
            public string Id { get; }

            public Query(string id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Query, Result<OrderDto>>
        {
            private readonly OrderProcessor _processor;

            public Handler(OrderProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Result<OrderDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.Get(request.Id);
            }
        }
    }
}