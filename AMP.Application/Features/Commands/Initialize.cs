using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Processors.Administration;
using MediatR;

namespace AMP.Application.Features.Commands
{
    public class Initialize
    {
        public class Command : IRequest
        {
            public Command()
            {
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly InitializeDbProcessor _processor;

            public Handler(InitializeDbProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _processor.InitializeDatabase();
                return Unit.Value;
            }
        }
    }
}