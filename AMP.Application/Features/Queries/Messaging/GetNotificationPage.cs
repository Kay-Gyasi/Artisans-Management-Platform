using AMP.Processors.PageDtos.Messaging;
using AMP.Processors.Processors.Messaging;
using AMP.Shared.Domain.Models;

namespace AMP.Application.Features.Queries.Messaging;

public class GetNotificationPage
{
    public class Query : IRequest<Result<PaginatedList<NotificationPageDto>>>
    {
        public PaginatedCommand Paginated { get; }

        public Query(PaginatedCommand paginated)
        {
            Paginated = paginated;
        }
    }

    public class Handler : IRequestHandler<Query, Result<PaginatedList<NotificationPageDto>>>
    {
        private readonly NotificationProcessor _processor;

        public Handler(NotificationProcessor processor)
        {
            _processor = processor;
        }
        
        public async Task<Result<PaginatedList<NotificationPageDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _processor.GetPageAsync(request.Paginated);
        }
    }
}