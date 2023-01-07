using AMP.Processors.PageDtos.Messaging;
using AMP.Processors.Processors.Messaging;
using AMP.Shared.Domain.Models;

namespace AMP.Application.Features.Queries.Messaging;

public class GetConversationPage
{
    public class Query : IRequest<Result<PaginatedList<ConversationPageDto>>>
    {
        public PaginatedCommand Paginated { get; }

        public Query(PaginatedCommand paginated)
        {
            Paginated = paginated;
        }
    }

    public class Handler : IRequestHandler<Query, Result<PaginatedList<ConversationPageDto>>>
    {
        private readonly ConversationProcessor _processor;

        public Handler(ConversationProcessor processor)
        {
            _processor = processor;
        }
        
        public async Task<Result<PaginatedList<ConversationPageDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _processor.GetPageAsync(request.Paginated);
        }
    }
}