using AMP.Processors.PageDtos.BusinessManagement;
using AMP.Processors.Processors.BusinessManagement;
using AMP.Shared.Domain.Models;

namespace AMP.Application.Features.Queries.BusinessManagement
{
    public class GetSchedule
    {
        public class Query : IRequest<PaginatedList<OrderPageDto>>
        {
            public PaginatedCommand Paginated { get; }
            public string UserId { get; }

            public Query(PaginatedCommand paginated, string userId)
            {
                Paginated = paginated;
                UserId = userId;
            }
        }

        public class Handler : IRequestHandler<Query, PaginatedList<OrderPageDto>>
        {
            private readonly OrderProcessor _processor;

            public Handler(OrderProcessor processor)
            {
                _processor = processor;
            }
            public async Task<PaginatedList<OrderPageDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetSchedule(request.Paginated, request.UserId);
            }
        }
    }
}