using AMP.Processors.Dtos.Messaging;
using AMP.Processors.Processors.Messaging;

namespace AMP.Application.Features.Queries.Messaging;

public class GetNotification
{
    public class Query : IRequest<Result<NotificationDto>>
    {
        public string Id { get; }

        public Query(string id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<Query, Result<NotificationDto>>
    {
        private readonly NotificationProcessor _processor;

        public Handler(NotificationProcessor processor)
        {
            _processor = processor;
        }
        
        public async Task<Result<NotificationDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _processor.GetAsync(request.Id);
        }
    }
}