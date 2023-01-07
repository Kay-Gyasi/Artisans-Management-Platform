using AMP.Processors.Dtos.Messaging;
using AMP.Processors.Processors.Messaging;

namespace AMP.Application.Features.Queries.Messaging;

public class GetConversation
{
    public class Query : IRequest<Result<ConversationDto>>
    {
        public string Id { get; }

        public Query(string id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<Query, Result<ConversationDto>>
    {
        private readonly ConversationProcessor _processor;

        public Handler(ConversationProcessor processor)
        {
            _processor = processor;
        }
        
        public async Task<Result<ConversationDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _processor.GetAsync(request.Id);
        }
    }
}