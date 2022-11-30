using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Commands;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands;

public class ResetPassword
{
    public class Command : IRequest
    {
        public ResetPasswordCommand ResetCommand { get; }

        public Command(ResetPasswordCommand resetCommand)
        {
            ResetCommand = resetCommand;
        }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly UserProcessor _processor;

        public Handler(UserProcessor processor)
        {
            _processor = processor;
        }
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            await _processor.ResetPassword(request.ResetCommand);
            return Unit.Value;
        }
    }
}