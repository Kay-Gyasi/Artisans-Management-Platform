using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Dtos;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Queries;

public class GetUserInvites
{
    public class Query : IRequest<List<InvitationDto>>
    {
        public string UserId { get; }

        public Query(string userId)
        {
            UserId = userId;
        }
    }
    
    public class Handler : IRequestHandler<Query, List<InvitationDto>>
    {
        private readonly InvitationProcessor _processor;

        public Handler(InvitationProcessor processor)
        {
            _processor = processor;
        }
        public async Task<List<InvitationDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _processor.GetUserInvites(request.UserId);
        }
    }
}