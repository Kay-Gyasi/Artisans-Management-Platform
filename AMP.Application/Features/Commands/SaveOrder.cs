using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Commands;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands
{
    public class SaveOrder
    {
        public class Command : IRequest<int>
        {
            public OrderCommand OrderCommand { get; }

            public Command(OrderCommand artisanCommand)
            {
                OrderCommand = artisanCommand;
            }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly OrderProcessor _processor;

            public Handler(OrderProcessor processor)
            {
                _processor = processor;
            }
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Save(request.OrderCommand);
            }
        }
    }
}