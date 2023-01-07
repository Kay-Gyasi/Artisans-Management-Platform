using AMP.Processors.Commands.UserManagement;
using AMP.Processors.Processors.UserManagement;
using LanguageExt;

namespace AMP.Application.Features.Commands.UserManagement
{
    public class AuthenticateUser
    {
        public class Command : IRequest<Option<SigninResponse>>
        {
            public SigninCommand Signin { get; }

            public Command(SigninCommand signin)
            {
                Signin = signin;
            }
        }

        public class Handler : IRequestHandler<Command, Option<SigninResponse>>
        {
            private readonly UserProcessor _processor;

            public Handler(UserProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Option<SigninResponse>> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Login(request.Signin);
            }
        }
    }
}