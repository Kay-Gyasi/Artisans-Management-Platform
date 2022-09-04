using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Dtos;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Queries
{
    public class GetArtisanByUser
    {
        public class Query : IRequest<ArtisanDto>
        {
            public string Id { get; }

            public Query(string id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Query, ArtisanDto>
        {
            private readonly ArtisanProcessor _processor;

            public Handler(ArtisanProcessor processor)
            {
                _processor = processor;
            }
            public async Task<ArtisanDto> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetByUserId(request.Id);
            }
        }
    }
}