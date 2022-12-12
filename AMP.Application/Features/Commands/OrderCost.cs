using AMP.Processors.Commands;
using AMP.Processors.Processors;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AMP.Application.Features.Commands
{
    public class OrderCost
    {

        public class Command : IRequest<Result<bool>>
        {
            public Command(SetCostCommand costCommand)
            {
                CostCommand = costCommand;
            }

            public SetCostCommand CostCommand { get; }
        }

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly OrderProcessor _processor;

            public Handler(OrderProcessor processor)
            {
                _processor = processor;
            }

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.SetCost(request.CostCommand);
            }
        }
    }
}
