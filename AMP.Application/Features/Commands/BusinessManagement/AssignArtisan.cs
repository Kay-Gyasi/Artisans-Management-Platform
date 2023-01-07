using AMP.Processors.Processors.BusinessManagement;

namespace AMP.Application.Features.Commands.BusinessManagement
{
    public class AssignArtisan
    {
        public class Command: IRequest<Result<bool>>
        {
            public string OrderId { get; }
            public string ArtisanId { get; }

            public Command(string orderId, string artisanId)
            {
                OrderId = orderId;
                ArtisanId = artisanId;
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
                return await _processor.AssignArtisan(request.OrderId, request.ArtisanId);
            }
        }
    }
}