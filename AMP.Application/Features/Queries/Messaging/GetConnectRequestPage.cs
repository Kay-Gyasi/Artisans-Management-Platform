using AMP.Processors.PageDtos.Messaging;
using AMP.Processors.Processors.Messaging;
using AMP.Shared.Domain.Models;

namespace AMP.Application.Features.Queries.Messaging;

public class GetConnectRequestPage
{
    public class Query : IRequest<Result<PaginatedList<ConnectRequestPageDto>>>
    {
        public PaginatedCommand Paginated { get; }

        public Query(PaginatedCommand paginated)
        {
            Paginated = paginated;
        }
    }

    public class Handler : IRequestHandler<Query, Result<PaginatedList<ConnectRequestPageDto>>>
    {
        private readonly ConnectRequestProcessor _processor;

        public Handler(ConnectRequestProcessor processor)
        {
            _processor = processor;
        }
        
        public async Task<Result<PaginatedList<ConnectRequestPageDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _processor.GetPageAsync(request.Paginated);
        }
    }
}