using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.ViewModels;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Queries
{
    public class GetLookup
    {
        public class Query : IRequest<List<Lookup>>
        {
            public LookupType Type { get; }

            public Query(LookupType type)
            {
                Type = type;
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
                return await _processor.GetLookup(request.Type);
            }
        }
    }
}