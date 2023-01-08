using System.Collections.Generic;
using AMP.Domain.ViewModels;

namespace AMP.Application.Features.Queries.BusinessManagement
{
    public class GetOpenOrdersLookup
    {
        public class Query : IRequest<List<Lookup>>
        {
            public string UserId { get; }

            public Query(string userId)
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