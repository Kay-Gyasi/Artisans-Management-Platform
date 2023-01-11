using AMP.Processors.PageDtos.UserManagement;

namespace AMP.Processors.PageDtos.Messaging;

public record ChatMessagePageDto(string Id, string SenderId, string ReceiverId, string Message, string ConversationId,
    UserPageDto Sender, UserPageDto Receiver, DateTime DateModified, ConversationPageDto Conversation);

public class ConversationPageDto
{
    public string Id { get; set; }
    public string FirstParticipantId { get; set; }
    public string SecondParticipantId { get; set; }
    public int UnreadMessages { get; set; }
    public UserPageDto FirstParticipant { get; set; }
    public UserPageDto SecondParticipant { get; set; }
    public DateTime DateModified { get; set; }
}

public record NotificationPageDto(string Id, string Message, string UserId, bool IsRead, UserPageDto User);
public record ConnectRequestPageDto(string Id, string InviterId, string InviteeId, UserPageDto Inviter);