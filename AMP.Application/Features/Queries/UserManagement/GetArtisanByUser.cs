﻿using AMP.Processors.Dtos.UserManagement;
using AMP.Processors.Processors.UserManagement;

namespace AMP.Application.Features.Queries.UserManagement
{
    public class GetArtisanByUser
    {
        public class Query : IRequest<Result<ArtisanDto>>
        {
            public string Id { get; }

            public Query(string id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Query, Result<ArtisanDto>>
        {
            private readonly ArtisanProcessor _processor;

            public Handler(ArtisanProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Result<ArtisanDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetByUserId(request.Id);
            }
        }
    }
}