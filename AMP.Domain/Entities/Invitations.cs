using System;
using AMP.Domain.Entities.Base;
using AMP.Domain.Enums;

namespace AMP.Domain.Entities;

public class Invitations : EntityBase
{
    public string UserId { get; private set; }
    public string InvitedPhone { get; private set; }
    public UserType Type { get; private set; }

    private Invitations(string userId, string invitedPhone, UserType type)
    {
        UserId = userId;
        InvitedPhone = invitedPhone;
        Type = type;
    }

    public static Invitations Create(string userId, string invitedPhone, UserType type)
        => new Invitations(userId, invitedPhone, type);

    public Invitations ByUserWithId(string userId)
    {
        UserId = userId;
        return this;
    }

    public Invitations ToPhone(string invitedPhone)
    {
        InvitedPhone = invitedPhone;
        return this;
    }
    
    public Invitations AsType(UserType type)
    {
        Type = type;
        return this;
    }
}