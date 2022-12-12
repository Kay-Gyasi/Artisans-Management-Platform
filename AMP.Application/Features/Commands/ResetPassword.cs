using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Commands;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands;

public class ResetPassword
{
    public class Command : IRequest<Result<bool>>
    {
        public ResetPasswordCommand ResetCommand { get; }

        public Command(ResetPasswordCommand resetCommand)
        {
            ResetCommand = resetCommand;
        }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly UserProcessor _processor;

        public Handler(UserProcessor processor)
        {
            _processor = processor;
        }
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            return await _processor.ResetPassword(request.ResetCommand);
        }
    }
}