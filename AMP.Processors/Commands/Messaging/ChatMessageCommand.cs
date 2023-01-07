namespace AMP.Processors.Commands.Messaging;

public class ChatMessageCommand
{
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }
    public string Message { get; set; }
    public string ConversationId { get; set; }
}
public record ConversationCommand(string Id, string FirstParticipantId, string SecondParticipantId);
public record NotificationCommand(string Id, string Message, string UserId, bool IsRead);
public record ConnectRequestCommand(string Id, string InviterId, string InviteeId);