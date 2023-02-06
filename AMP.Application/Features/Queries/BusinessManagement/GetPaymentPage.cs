using AMP.Processors.PageDtos.BusinessManagement;
using AMP.Processors.Processors.BusinessManagement;
using AMP.Shared.Domain.Models;
using OneOf;

namespace AMP.Application.Features.Queries.BusinessManagement
{
    public class GetPaymentPage
    {
        public class Query : IRequest<OneOf<PaginatedList<PaymentPageDto>, ArtisanPaymentPageDto>>
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

        public class Handler : IRequestHandler<Query, OneOf<PaginatedList<PaymentPageDto>, ArtisanPaymentPageDto>>
        {
            private readonly PaymentProcessor _processor;

            public Handler(PaymentProcessor processor)
            {
                _processor = processor;
            }
            public async Task<OneOf<PaginatedList<PaymentPageDto>, ArtisanPaymentPageDto>> 
                Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetPage(request.Command, request.UserId, request.Role);
            }
        }
    }
}