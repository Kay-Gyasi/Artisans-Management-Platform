using AMP.Processors.Processors.Messaging;

namespace AMP.Application.Features.Commands.Messaging;

public class DeleteChatMessage
{
    public class Command : IRequest<Result<bool>>
    {
        public string Id { get; }

        public Command(string id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly ChatMessageProcessor _processor;

        public Handler(ChatMessageProcessor processor)
        {
            _processor = processor;
        }
        
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            return await _processor.Delete(request.Id);
        }
    }
}