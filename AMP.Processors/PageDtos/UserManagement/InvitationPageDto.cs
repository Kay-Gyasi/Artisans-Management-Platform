namespace AMP.Processors.PageDtos.UserManagement;

public class InvitationPageDto
{
    public string InvitedPhone { get; set; }
    public UserType Type { get; set; }
    public DateTime DateCreated { get; set; }
}