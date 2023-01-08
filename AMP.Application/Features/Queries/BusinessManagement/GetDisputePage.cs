using AMP.Processors.PageDtos.BusinessManagement;
using AMP.Processors.Processors.BusinessManagement;
using AMP.Shared.Domain.Models;

namespace AMP.Application.Features.Queries.BusinessManagement
{
    public class GetDisputePage
    {
        public class Query : IRequest<PaginatedList<DisputePageDto>>
        {
            public PaginatedCommand Command { get; }
            public string UserId { get; }

            public Query(PaginatedCommand command, string userId)
            {
                Command = command;
                UserId = userId;
            }
        }

        public class Handler : IRequestHandler<Query, PaginatedList<DisputePageDto>>
        {
            private readonly DisputeProcessor _processor;

            public Handler(DisputeProcessor processor)
            {
                _processor = processor;
            }
            public async Task<PaginatedList<DisputePageDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetPage(request.Command, request.UserId);
            }
        }
    }
}