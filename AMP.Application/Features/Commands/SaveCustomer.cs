using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Commands;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands
{
    public class SaveCustomer
    {
        public class Command : IRequest<int>
        {
            public CustomerCommand CustomerCommand { get; }

            public Command(CustomerCommand artisanCommand)
            {
                CustomerCommand = artisanCommand;
            }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly CustomerProcessor _processor;

            public Handler(CustomerProcessor processor)
            {
                _processor = processor;
            }
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Save(request.CustomerCommand);
            }
        }
    }
}