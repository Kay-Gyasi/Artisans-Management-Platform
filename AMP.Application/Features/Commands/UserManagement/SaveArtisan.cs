using AMP.Processors.Commands.UserManagement;
using AMP.Processors.Processors.UserManagement;

namespace AMP.Application.Features.Commands.UserManagement
{
    public class SaveArtisan
    {
        public class Command : IRequest<Result<string>>
        {
            public ArtisanCommand ArtisanCommand { get; }

            public Command(ArtisanCommand artisanCommand)
            {
                ArtisanCommand = artisanCommand;
            }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly ArtisanProcessor _processor;

            public Handler(ArtisanProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Save(request.ArtisanCommand);
            }
        }
    }
}