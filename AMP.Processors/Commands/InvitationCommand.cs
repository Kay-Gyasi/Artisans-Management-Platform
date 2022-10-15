using AMP.Domain.Enums;

namespace AMP.Processors.Commands;

public class InvitationCommand
{
    public string InvitedPhone { get; set; }
    public UserType Type { get; set; }
}