using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Commands;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands
{
    public class SaveDispute
    {
        public class Command : IRequest<int>
        {
            public DisputeCommand DisputeCommand { get; }

            public Command(DisputeCommand artisanCommand)
            {
                DisputeCommand = artisanCommand;
            }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly DisputeProcessor _processor;

            public Handler(DisputeProcessor processor)
            {
                _processor = processor;
            }
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Save(request.DisputeCommand);
            }
        }
    }
}