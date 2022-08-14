using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands
{
    public class AcceptOrder
    {
        public class Command : IRequest
        {
            public int OrderId { get; }

            public Command(int orderId)
            {
                OrderId = orderId;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly OrderProcessor _processor;

            public Handler(OrderProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _processor.AcceptRequest(request.OrderId);
                return Unit.Value;
            }
        }
    }
}