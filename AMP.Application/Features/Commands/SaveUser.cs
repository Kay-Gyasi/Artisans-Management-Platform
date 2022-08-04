using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Commands;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands
{
    public class SaveUser
    {
        public class Command : IRequest<int>
        {
            public UserCommand UserCommand { get; }

            public Command(UserCommand artisanCommand)
            {
                UserCommand = artisanCommand;
            }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly UserProcessor _processor;

            public Handler(UserProcessor processor)
            {
                _processor = processor;
            }
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Save(request.UserCommand);
            }
        }
    }
}