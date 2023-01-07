using AMP.Processors.Dtos.Messaging;
using AMP.Processors.Processors.Messaging;

namespace AMP.Application.Features.Queries.Messaging;

public class GetConnectRequest
{
    public class Query : IRequest<Result<ConnectRequestDto>>
    {
        public string Id { get; }

        public Query(string id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<Query, Result<ConnectRequestDto>>
    {
        private readonly ConnectRequestProcessor _processor;

        public Handler(ConnectRequestProcessor processor)
        {
            _processor = processor;
        }
        
        public async Task<Result<ConnectRequestDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _processor.GetAsync(request.Id);
        }
    }
}