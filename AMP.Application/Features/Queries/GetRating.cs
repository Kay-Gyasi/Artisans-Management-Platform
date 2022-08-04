using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Dtos;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Queries
{
    public class GetRating
    {
        public class Query : IRequest<RatingDto>
        {
            public int Id { get; }

            public Query(int id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Query, RatingDto>
        {
            private readonly RatingProcessor _processor;

            public Handler(RatingProcessor processor)
            {
                _processor = processor;
            }
            public async Task<RatingDto> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.Get(request.Id);
            }
        }
    }
}