using AMP.Processors.Processors.BusinessManagement;

namespace AMP.Application.Features.Commands.BusinessManagement
{
    public class DeleteRating
    {
        public class Command : IRequest
        {
            public string Id { get; }

            public Command(string id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly RatingProcessor _processor;

            public Handler(RatingProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _processor.Delete(request.Id);
                return Unit.Value;
            }
        }
    }
}