using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Commands;
using AMP.Processors.Processors;
using AMP.Processors.Responses;
using MediatR;

namespace AMP.Application.Features.Queries
{
    public class InsertOrder
    {
        public class Command : IRequest<Result<InsertOrderResponse>>
        {
            public OrderCommand Payload { get; }

            public Command(OrderCommand command)
            {
                Payload = command;
            }
        }

        public class Handler : IRequestHandler<Command, Result<InsertOrderResponse>>
        {
            private readonly OrderProcessor _processor;

            public Handler(OrderProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Result<InsertOrderResponse>> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Insert(request.Payload);
            }
        }
    }
}
