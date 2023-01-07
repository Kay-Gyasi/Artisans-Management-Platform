namespace AMP.Processors.Dtos.UserManagement;

public class InvitationDto
{
    public string InvitedPhone { get; set; }
    public UserType Type { get; set; }
    public DateTime DateCreated { get; set; }
}