using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.PageDtos;
using AMP.Processors.Processors;
using AMP.Shared.Domain.Models;
using MediatR;

namespace AMP.Application.Features.Queries
{
    public class GetPaymentPage
    {
        public class Query : IRequest<PaginatedList<PaymentPageDto>>
        {
            public PaginatedCommand Command { get; }
            public string UserId { get; }
            public string Role { get; }

            public Query(PaginatedCommand command, string userId, string role)
            {
                Command = command;
                UserId = userId;
                Role = role;
            }
        }

        public class Handler : IRequestHandler<Query, PaginatedList<PaymentPageDto>>
        {
            private readonly PaymentProcessor _processor;

            public Handler(PaymentProcessor processor)
            {
                _processor = processor;
            }
            public async Task<PaginatedList<PaymentPageDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetPage(request.Command, request.UserId, request.Role);
            }
        }
    }
}