using AMP.Processors.PageDtos;
using AMP.Shared.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Processors;

namespace AMP.Application.Features.Queries
{
    public class ArtisanData
    {
        public class Query : IRequest<PaginatedList<ArtisanPageDto>>
        {
            public PaginatedCommand Command { get; }

            public Query(PaginatedCommand command)
            {
                Command = command;
            }
        }

        public class Handler : IRequestHandler<Query, PaginatedList<ArtisanPageDto>>
        {
            private readonly ArtisanProcessor _processor;

            public Handler(ArtisanProcessor processor)
            {
                _processor = processor;
            }
            public async Task<PaginatedList<ArtisanPageDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetPage(request.Command);
            }
        }

        // implement validation class
    }
}