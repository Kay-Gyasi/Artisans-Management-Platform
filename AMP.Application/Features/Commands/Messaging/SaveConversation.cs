using AMP.Processors.Commands.Messaging;
using AMP.Processors.Processors.Messaging;

namespace AMP.Application.Features.Commands.Messaging;

public class SaveConversation
{
    public class Command : IRequest<Result<string>>
    {
        public ConversationCommand Payload { get; }

        public Command(ConversationCommand payload)
        {
            Payload = payload;
        }
    }

    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly ConversationProcessor _processor;

        public Handler(ConversationProcessor processor)
        {
            _processor = processor;
        }
        
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            return await _processor.Save(request.Payload);
        }
    }
}