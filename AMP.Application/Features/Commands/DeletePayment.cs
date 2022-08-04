﻿using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands
{
    public class DeletePayment
    {
        public class Command : IRequest
        {
            public int Id { get; }

            public Command(int id)
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