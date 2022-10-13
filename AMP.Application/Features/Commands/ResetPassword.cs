using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Commands;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands;

public class ResetPassword
{
    public class Command : IRequest<bool>
    {
        public ResetPasswordCommand ResetCommand { get; }

        public Command(ResetPasswordCommand resetCommand)
        {
            ResetCommand = resetCommand;
        }
    }

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly UserProcessor _processor;

        public Handler(UserProcessor processor)
        {
            _processor = processor;
        }
        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            return await _processor.ResetPassword(request.ResetCommand);
        }
    }
}