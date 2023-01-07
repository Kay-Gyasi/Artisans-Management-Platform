using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.ViewModels;
using AMP.Processors.Processors;
using AMP.Processors.Processors.UserManagement;
using MediatR;

namespace AMP.Application.Features.Queries
{
    public class GetArtisansWhoHaveWorkedForCustomer
    {
        public class Query : IRequest<Result<List<Lookup>>>
        {
            public string Id { get; }

            public Query(string id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Query, Result<List<Lookup>>>
        {
            private readonly ArtisanProcessor _processor;

            public Handler(ArtisanProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Result<List<Lookup>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetArtisansWhoHaveWorkedForCustomer(request.Id);
            }
        }
    }
}