using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.PageDtos;
using AMP.Processors.Processors;
using AMP.Shared.Domain.Models;
using MediatR;

namespace AMP.Application.Features.Queries
{
    public class GetDisputePage
    {
        public class Query : IRequest<PaginatedList<DisputePageDto>>
        {
            public PaginatedCommand Command { get; }
            public string UserId { get; }

            public Query(PaginatedCommand command, string userId)
            {
                Command = command;
                UserId = userId;
            }
        }

        public class Handler : IRequestHandler<Query, PaginatedList<DisputePageDto>>
        {
            private readonly DisputeProcessor _processor;

            public Handler(DisputeProcessor processor)
            {
                _processor = processor;
            }
            public async Task<PaginatedList<DisputePageDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetPage(request.Command, request.UserId);
            }
        }
    }
}