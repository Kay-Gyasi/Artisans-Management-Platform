﻿using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands
{
    public class CancelOrder
    {
        public class Command : IRequest
        {
            public string OrderId { get; }

            public Command(string orderId)
            {
                OrderId = orderId;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly OrderProcessor _processor;

            public Handler(OrderProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _processor.CancelRequest(request.OrderId);
                return Unit.Value;
            }
        }
    }
}