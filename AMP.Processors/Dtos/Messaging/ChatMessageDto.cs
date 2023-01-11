using AMP.Processors.Dtos.UserManagement;
using AMP.Processors.PageDtos.UserManagement;

namespace AMP.Processors.Dtos.Messaging;

public record ChatMessageDto(string Id, string SenderId, string ReceiverId, string Message, string ConversationId,
    DateTime DateModified, ConversationDto Conversation);

public class ConversationDto
{
    public string Id { get; set; }
    public string FirstParticipantId { get; set; }
    public string SecondParticipantId { get; set; }
    public UserPageDto FirstParticipant { get; set; }
    public UserPageDto SecondParticipant { get; set; }
    public DateTime DateModified { get; set; }
    public List<ChatMessageDto> Messages { get; set; }
}

public record NotificationDto(string Id, string Message, string UserId, bool IsRead, UserDto User);
public record ConnectRequestDto(string Id, string InviterId, string InviteeId, UserDto Inviter);