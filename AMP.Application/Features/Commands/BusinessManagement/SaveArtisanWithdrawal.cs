using AMP.Processors.Processors.BusinessManagement;

namespace AMP.Application.Features.Commands.BusinessManagement;

public class SaveArtisanWithdrawal
{
    public class Command : IRequest<Result<bool>>
    {
        
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly PaymentWithdrawalProcessor _processor;

        public Handler(PaymentWithdrawalProcessor processor)
        {
            _processor = processor;
        }
        
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            return await _processor.SaveArtisanWithdrawalAsync();
        }
    }
}