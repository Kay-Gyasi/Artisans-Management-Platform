using AMP.Processors.Commands.BusinessManagement;
using AMP.Processors.Processors.BusinessManagement;
using AMP.Processors.Responses;

namespace AMP.Application.Features.Queries.BusinessManagement
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
