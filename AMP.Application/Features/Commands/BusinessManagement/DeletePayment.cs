using AMP.Processors.Processors.BusinessManagement;

namespace AMP.Application.Features.Commands.BusinessManagement
{
    public class DeletePayment
    {
        public class Command : IRequest
        {
            public string Id { get; }

            public Command(string id)
            {
                Id = id;
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
                await _processor.Delete(request.Id);
                return Unit.Value;
            }
        }
    }
}