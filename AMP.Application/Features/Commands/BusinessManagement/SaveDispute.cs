using AMP.Processors.Commands.BusinessManagement;
using AMP.Processors.Processors.BusinessManagement;

namespace AMP.Application.Features.Commands.BusinessManagement
{
    public class SaveDispute
    {
        public class Command : IRequest<Result<string>>
        {
            public DisputeCommand DisputeCommand { get; }
            public string UserId { get; }

            public Command(DisputeCommand artisanCommand, string userId)
            {
                DisputeCommand = artisanCommand;
                UserId = userId;
            }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly DisputeProcessor _processor;

            public Handler(DisputeProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Save(request.DisputeCommand, request.UserId);
            }
        }
    }
}