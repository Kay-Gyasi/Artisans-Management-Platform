using System.Collections.Generic;
using AMP.Processors.Dtos.UserManagement;
using AMP.Processors.Processors.UserManagement;

namespace AMP.Application.Features.Queries.UserManagement;

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