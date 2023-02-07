using AMP.Processors.Processors.UserManagement;

namespace AMP.Application.Features.Commands.UserManagement;

public class CreatePaymentCustomers
{
    public class Command : IRequest<Result<bool>>
    {
        public Command()
        {
            
        }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly UserProcessor _processor;

        public Handler(UserProcessor processor)
        {
            _processor = processor;
        }
        
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            return await _processor.CreatePaymentCustomers();
        }
    }
}