using AMP.Processors.Commands.BusinessManagement;
using AMP.Processors.Processors.BusinessManagement;

namespace AMP.Application.Features.Commands.BusinessManagement
{
    public class SaveService
    {
        public class Command : IRequest<string>
        {
            public ServiceCommand ServiceCommand { get; }

            public Command(ServiceCommand artisanCommand)
            {
                ServiceCommand = artisanCommand;
            }
        }

        public class Handler : IRequestHandler<Command, string>
        {
            private readonly ServiceProcessor _processor;

            public Handler(ServiceProcessor processor)
            {
                _processor = processor;
            }
            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Save(request.ServiceCommand);
            }
        }
    }
}