using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.PageDtos;
using AMP.Processors.Processors;
using AMP.Shared.Domain.Models;
using MediatR;

namespace AMP.Application.Features.Queries
{
    public class GetArtisanRatingPage
    {
        public class Query : IRequest<PaginatedList<RatingPageDto>>
        {
            public PaginatedCommand Command { get; }
            public string UserId { get; }

            public Query(PaginatedCommand command, string userId)
            {
                Command = command;
                UserId = userId;
            }
        }

        public class Handler : IRequestHandler<Query, PaginatedList<RatingPageDto>>
        {
            private readonly RatingProcessor _processor;

            public Handler(RatingProcessor processor)
            {
                _processor = processor;
            }
            public async Task<PaginatedList<RatingPageDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetArtisanRatingPage(request.Command, request.UserId);
            }
        }
    }
}