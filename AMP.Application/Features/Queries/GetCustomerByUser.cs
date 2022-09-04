using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Dtos;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Queries
{
    public class GetCustomerByUser
    {
        public class Query : IRequest<CustomerDto>
        {
            public string Id { get; }

            public Query(string id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Query, CustomerDto>
        {
            private readonly CustomerProcessor _processor;

            public Handler(CustomerProcessor processor)
            {
                _processor = processor;
            }
            public async Task<CustomerDto> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetByUserId(request.Id);
            }
        }
    }
}