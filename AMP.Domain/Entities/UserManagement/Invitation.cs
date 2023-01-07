using AMP.Domain.Entities.Base;
using AMP.Domain.Enums;

namespace AMP.Domain.Entities.UserManagement;

public class Invitation : EntityBase
{
    public string UserId { get; private set; }
    public string InvitedPhone { get; private set; }
    public UserType Type { get; private set; }

    private Invitation(string userId, string invitedPhone, UserType type)
    {
        UserId = userId;
        InvitedPhone = invitedPhone;
        Type = type;
    }

    public static Invitation Create(string userId, string invitedPhone, UserType type)
        => new Invitation(userId, invitedPhone, type);

    public Invitation ByUserWithId(string userId)
    {
        UserId = userId;
        return this;
    }

    public Invitation ToPhone(string invitedPhone)
    {
        InvitedPhone = invitedPhone;
        return this;
    }
    
    public Invitation AsType(UserType type)
    {
        Type = type;
        return this;
    }
}