using AMP.Processors.PageDtos.BusinessManagement;
using AMP.Processors.Processors.BusinessManagement;
using AMP.Shared.Domain.Models;

namespace AMP.Application.Features.Queries.BusinessManagement
{
    public class GetOrderPage
    {
        public class Query : IRequest<PaginatedList<OrderPageDto>>
        {
            public PaginatedCommand Command { get; }

            public Query(PaginatedCommand command)
            {
                Command = command;
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
                return await _processor.GetPage(request.Command);
            }
        }
    }
}