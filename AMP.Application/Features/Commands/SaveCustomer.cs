using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Commands;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands
{
    public class SaveCustomer
    {
        public class Command : IRequest<Result<string>>
        {
            public CustomerCommand CustomerCommand { get; }

            public Command(CustomerCommand artisanCommand)
            {
                CustomerCommand = artisanCommand;
            }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly CustomerProcessor _processor;

            public Handler(CustomerProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Save(request.CustomerCommand);
            }
        }
    }
}