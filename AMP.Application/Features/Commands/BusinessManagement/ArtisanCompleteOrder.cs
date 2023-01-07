using AMP.Processors.Processors.BusinessManagement;

namespace AMP.Application.Features.Commands.BusinessManagement
{
    public class ArtisanCompleteOrder
    {
        public class Command : IRequest<Result<bool>>
        {
            public string OrderId { get; }

            public Command(string orderId)
            {
                OrderId = orderId;
            }
        }

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly OrderProcessor _processor;

            public Handler(OrderProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.ArtisanComplete(request.OrderId);
            }
        }
    }
}