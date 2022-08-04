using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.PageDtos;
using AMP.Processors.Processors;
using AMP.Shared.Domain.Models;
using MediatR;

namespace AMP.Application.Features.Queries
{
    public class GetSchedule
    {
        public class Query : IRequest<PaginatedList<OrderPageDto>>
        {
            public PaginatedCommand Paginated { get; }
            public int UserId { get; }

            public Query(PaginatedCommand paginated, int userId)
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