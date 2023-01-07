using AMP.Processors.Commands.Messaging;
using AMP.Processors.Processors.Messaging;

namespace AMP.Application.Features.Commands.Messaging;

public class SaveConnectRequest
{
    public class Command : IRequest<Result<string>>
    {
        public ConnectRequestCommand Payload { get; }

        public Command(ConnectRequestCommand payload)
        {
            Payload = payload;
        }
    }

    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly ConnectRequestProcessor _processor;

        public Handler(ConnectRequestProcessor processor)
        {
            _processor = processor;
        }
        
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            return await _processor.Save(request.Payload);
        }
    }
}