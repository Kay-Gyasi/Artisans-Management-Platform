using AMP.Domain.Entities.Base;
using AMP.Domain.Entities.UserManagement;

namespace AMP.Domain.Entities.Messaging;

public class ConnectRequest : EntityBase
{
    private ConnectRequest(string inviterId, string inviteeId)
    {
        InviterId = inviterId;
        InviteeId = inviteeId;
    }
    
    public string InviterId { get; private set; }
    public string InviteeId { get; private set; }
    public bool IsAccepted { get; set; }
    public User Inviter { get; set; }
    public User Invitee { get; set; }

    public static ConnectRequest Create(string inviterId, string inviteeId)
    {
        return new ConnectRequest(inviterId, inviteeId);
    }

    public ConnectRequest By(string inviterId)
    {
        InviterId = inviterId;
        return this;
    }

    public ConnectRequest To(string inviteeId)
    {
        InviteeId = inviteeId;
        return this;
    }

    public ConnectRequest IsAcceptedd()
    {
        IsAccepted = true;
        return this;
    }
}