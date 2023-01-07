using AMP.Processors.Commands.UserManagement;
using AMP.Processors.Processors.UserManagement;

namespace AMP.Application.Features.Commands.UserManagement;

public class AddInvite
{
    public class Command : IRequest<bool>
    {
        public InvitationCommand Invite { get; }
        public string UserId { get; }

        public Command(InvitationCommand invite, string userId)
        {
            Invite = invite;
            UserId = userId;
        }
    }
    
    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly InvitationProcessor _processor;

        public Handler(InvitationProcessor processor)
        {
            _processor = processor;
        }
        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            return await _processor.AddInvite(request.Invite, request.UserId);
        }
    }
}