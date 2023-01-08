using AMP.Processors.Dtos.BusinessManagement;
using AMP.Processors.Processors.BusinessManagement;

namespace AMP.Application.Features.Queries.BusinessManagement
{
    public class GetRating
    {
        public class Query : IRequest<RatingDto>
        {
            public string Id { get; }

            public Query(string id)
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