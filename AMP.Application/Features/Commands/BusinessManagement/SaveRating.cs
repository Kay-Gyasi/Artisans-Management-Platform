using AMP.Processors.Commands.BusinessManagement;
using AMP.Processors.Processors.BusinessManagement;

namespace AMP.Application.Features.Commands.BusinessManagement
{
    public class SaveRating
    {
        public class Command : IRequest<string>
        {
            public RatingCommand RatingCommand { get; }

            public Command(RatingCommand artisanCommand)
            {
                RatingCommand = artisanCommand;
            }
        }

        public class Handler : IRequestHandler<Command, string>
        {
            private readonly RatingProcessor _processor;

            public Handler(RatingProcessor processor)
            {
                _processor = processor;
            }
            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Save(request.RatingCommand);
            }
        }
    }
}