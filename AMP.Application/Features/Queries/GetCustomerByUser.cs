using AMP.Processors.Dtos;

namespace AMP.Application.Features.Queries
{
    public class GetCustomerByUser
    {
        public class Query : IRequest<Result<CustomerDto>>
        {
            public string Id { get; }

            public Query(string id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Query, Result<CustomerDto>>
        {
            private readonly CustomerProcessor _processor;

            public Handler(CustomerProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Result<CustomerDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetByUserId(request.Id);
            }
        }
    }
}