using AMP.Processors.Commands.UserManagement;
using AMP.Processors.Processors.UserManagement;

namespace AMP.Application.Features.Commands.UserManagement
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