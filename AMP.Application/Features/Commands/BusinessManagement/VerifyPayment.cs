using AMP.Processors.Commands.BusinessManagement;
using AMP.Processors.Processors.BusinessManagement;

namespace AMP.Application.Features.Commands.BusinessManagement
{
    public class VerifyPayment
    {
        public class Command : IRequest
        {
            public VerifyPaymentCommand Payment { get; }

            public Command(VerifyPaymentCommand payment)
            {
                Payment = payment;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly PaymentProcessor _processor;

            public Handler(PaymentProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _processor.Verify(request.Payment);
                return Unit.Value;
            }
        }
    }
}