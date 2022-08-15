using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Commands;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands
{
    public class SavePayment
    {
        public class Command : IRequest<int>
        {
            public PaymentCommand PaymentCommand { get; }
            public int UserId { get; }

            public Command(PaymentCommand artisanCommand, int userId)
            {
                PaymentCommand = artisanCommand;
                UserId = userId;
            }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly PaymentProcessor _processor;

            public Handler(PaymentProcessor processor)
            {
                _processor = processor;
            }
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Save(request.PaymentCommand, request.UserId);
            }
        }
    }
}