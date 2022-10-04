using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Commands;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands
{
    public class SavePayment
    {
        public class Command : IRequest<string>
        {
            public PaymentCommand PaymentCommand { get; }

            public Command(PaymentCommand artisanCommand)
            {
                PaymentCommand = artisanCommand;
            }
        }

        public class Handler : IRequestHandler<Command, string>
        {
            private readonly PaymentProcessor _processor;

            public Handler(PaymentProcessor processor)
            {
                _processor = processor;
            }
            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Save(request.PaymentCommand);
            }
        }
    }
}