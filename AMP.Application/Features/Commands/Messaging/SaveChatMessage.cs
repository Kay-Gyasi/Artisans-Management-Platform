using AMP.Processors.Commands.Messaging;
using AMP.Processors.Processors.Messaging;

namespace AMP.Application.Features.Commands.Messaging;

public class SaveChatMessage
{
    public class Command : IRequest<Result<string>>
    {
        public ChatMessageCommand Payload { get; }

        public Command(ChatMessageCommand payload)
        {
            Payload = payload;
        }
    }

    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly ChatMessageProcessor _processor;

        public Handler(ChatMessageProcessor processor)
        {
            _processor = processor;
        }
        
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            return await _processor.Save(request.Payload);
        }
    }
}