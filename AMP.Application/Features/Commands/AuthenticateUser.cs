using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Commands;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands
{
    public class AuthenticateUser
    {
        public class Command : IRequest<SigninResponse>
        {
            public SigninCommand Signin { get; }

            public Command(SigninCommand signin)
            {
                Signin = signin;
            }
        }

        public class Handler : IRequestHandler<Command, SigninResponse>
        {
            private readonly UserProcessor _processor;

            public Handler(UserProcessor processor)
            {
                _processor = processor;
            }
            public async Task<SigninResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Login(request.Signin);
            }
        }
    }
}