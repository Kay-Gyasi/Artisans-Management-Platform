using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.ViewModels;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Queries
{
    public class GetOpenOrdersLookup
    {
        public class Query : IRequest<List<Lookup>>
        {
            public int UserId { get; }

            public Query(int userId)
            {
                UserId = userId;
            }
        }

        public class Handler : IRequestHandler<Query, List<Lookup>>
        {
            private readonly LookupProcessor _processor;

            public Handler(LookupProcessor processor)
            {
                _processor = processor;
            }
            public async Task<List<Lookup>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetOpenOrdersLookup(request.UserId);
            }
        }
    }
}