using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Commands;
using AMP.Processors.Dtos;
using AMP.Processors.Messaging;
using AMP.Processors.Processors.Base;
using AMP.Processors.Repositories.UoW;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace AMP.Processors.Processors;

[Processor]
public class InvitationProcessor : ProcessorBase
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

        var invitation = Invitations.Create(userId, command.InvitedPhone, command.Type);
        await Uow.Invitations.InsertAsync(invitation.CreatedOn());
        await Uow.SaveChangesAsync();
        await SendInvite(command, user);
        return true;
    }

    public async Task<List<InvitationDto>> GetUserInvites(string userId)
    {
        return Mapper.Map<List<InvitationDto>>(await Uow.Invitations.GetUserInvites(userId));
    }

    private async Task SendInvite(InvitationCommand command, Users user)
    {
        var message = MessageGenerator.SendInvite(command, user.DisplayName);
        await _smsMessaging.Send(new SmsCommand {Message = message.Item1, Recipients = new [] {message.Item2}});
    }
}