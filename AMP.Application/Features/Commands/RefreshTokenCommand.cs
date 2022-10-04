using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Commands;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands
{
    public class RefreshTokenCommand
    {
        public class Command : IRequest<SigninResponse>
        {
            public string UserId { get; }

            public Command(string userId)
            {
                UserId = userId;
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
                return await _processor.GetRefreshToken(request.UserId);
            }
        }
    }
}