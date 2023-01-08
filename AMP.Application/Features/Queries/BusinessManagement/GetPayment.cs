using AMP.Processors.Dtos.BusinessManagement;
using AMP.Processors.Processors.BusinessManagement;

namespace AMP.Application.Features.Queries.BusinessManagement
{
    public class GetPayment
    {
        public class Query : IRequest<PaymentDto>
        {
            public string Id { get; }

            public Query(string id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Query, PaymentDto>
        {
            private readonly PaymentProcessor _processor;

            public Handler(PaymentProcessor processor)
            {
                _processor = processor;
            }
            public async Task<PaymentDto> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.Get(request.Id);
            }
        }
    }
}