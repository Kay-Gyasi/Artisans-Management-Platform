using AMP.Processors.Commands;
using AMP.Processors.Processors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AMP.Application.Features.Commands
{
    public class OrderCost
    {

        public class Command : IRequest
        {
            public Command(SetCostCommand costCommand)
            {
                CostCommand = costCommand;
            }

            public SetCostCommand CostCommand { get; }
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
                await _processor.SetCost(request.CostCommand);
                return Unit.Value;
            }
        }
    }
}
