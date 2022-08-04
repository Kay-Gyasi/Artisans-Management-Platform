﻿using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands
{
    public class AssignArtisan
    {
        public class Command: IRequest
        {
            public int OrderId { get; }
            public int ArtisanId { get; }

            public Command(int orderId, int artisanId)
            {
                OrderId = orderId;
                ArtisanId = artisanId;
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
                await _processor.AssignArtisan(request.OrderId, request.ArtisanId);
                return Unit.Value;
            }
        }
    }
}