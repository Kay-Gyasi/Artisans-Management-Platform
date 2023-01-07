using AMP.Processors.Commands.BusinessManagement;
using AMP.Processors.Processors.BusinessManagement;

namespace AMP.Application.Features.Commands.BusinessManagement
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