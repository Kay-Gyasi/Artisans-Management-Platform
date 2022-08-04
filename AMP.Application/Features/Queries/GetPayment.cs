using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Dtos;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Queries
{
    public class GetPayment
    {
        public class Query : IRequest<PaymentDto>
        {
            public int Id { get; }

            public Query(int id)
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