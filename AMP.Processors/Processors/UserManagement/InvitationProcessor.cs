using AMP.Domain.Entities.UserManagement;
using AMP.Processors.Commands.UserManagement;
using AMP.Processors.Dtos.UserManagement;

namespace AMP.Processors.Processors.UserManagement;

[Processor]
public class InvitationProcessor : Processor
{
    private readonly ISmsMessaging _smsMessaging;

    public InvitationProcessor(ISmsMessaging smsMessaging, IUnitOfWork uow, IMapper mapper, IMemoryCache cache) 
        : base(uow, mapper, cache)
    {
        _smsMessaging = smsMessaging;
    }

    public async Task<bool> AddInvite(InvitationCommand command, string userId)
    {
        var user = await Uow.Users.GetAsync(userId);
        if (user is null) return false;
        var invitedUserExists = await Uow.Users.Exists(command.InvitedPhone);
        if (invitedUserExists) return false;

        var invitation = Invitation.Create(userId, command.InvitedPhone, command.Type);
        await Uow.Invitations.InsertAsync(invitation);
        await Uow.SaveChangesAsync();
        await SendInvite(command, user);
        return true;
    }

    public async Task<List<InvitationDto>> GetUserInvites(string userId)
    {
        return Mapper.Map<List<InvitationDto>>(await Uow.Invitations.GetUserInvites(userId));
    }

    private async Task SendInvite(InvitationCommand command, User user)
    {
        var message = MessageGenerator.SendInvite(command, user.DisplayName);
        await _smsMessaging.Send(new SmsCommand {Message = message.Item1, Recipients = new [] {message.Item2}});
    }
}