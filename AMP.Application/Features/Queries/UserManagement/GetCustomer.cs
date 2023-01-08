using AMP.Processors.Dtos.UserManagement;
using AMP.Processors.Processors.UserManagement;

namespace AMP.Application.Features.Queries.UserManagement
{
    public class GetCustomer
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
                return await _processor.Get(request.Id);
            }
        }
    }
}