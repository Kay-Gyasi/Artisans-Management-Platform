namespace AMP.Application.Features.Commands
{
    public class DeleteCustomer
    {
        public class Command : IRequest<Result<bool>>
        {
            public string Id { get; }

            public Command(string id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly CustomerProcessor _processor;

            public Handler(CustomerProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Delete(request.Id);
            }
        }
    }
}