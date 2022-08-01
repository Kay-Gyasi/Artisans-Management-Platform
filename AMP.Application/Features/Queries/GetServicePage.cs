using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.PageDtos;
using AMP.Processors.Processors;
using AMP.Shared.Domain.Models;
using MediatR;

namespace AMP.Application.Features.Queries
{
    public class GetServicePage
    {
        public class Query : IRequest<PaginatedList<ServicePageDto>>
        {
            public PaginatedCommand Command { get; }

            public Query(PaginatedCommand command)
            {
                Command = command;
            }
        }

        public class Handler : IRequestHandler<Query, PaginatedList<ServicePageDto>>
        {
            private readonly ServiceProcessor _processor;

            public Handler(ServiceProcessor processor)
            {
                _processor = processor;
            }
            public async Task<PaginatedList<ServicePageDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetPage(request.Command);
            }
        }
    }
}