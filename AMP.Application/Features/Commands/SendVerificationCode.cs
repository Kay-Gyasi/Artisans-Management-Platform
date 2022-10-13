using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands;

public class SendVerificationCode
{
    public class Command : IRequest
    {
        public string Phone { get; }

        public Command(string phone)
        {
            Phone = phone;
        }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly RegistrationProcessor _processor;

        public Handler(RegistrationProcessor processor)
        {
            _processor = processor;
        }
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            await _processor.SendVerificationLink(request.Phone, null);
            return Unit.Value;
        }
    }
}