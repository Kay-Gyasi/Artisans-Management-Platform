using AMP.Processors.PageDtos.UserManagement;

namespace AMP.Processors.PageDtos.Messaging;

public record ChatMessagePageDto(string Id, string SenderId, string ReceiverId, string Message, string ConversationId,
    UserPageDto Sender, UserPageDto Receiver, ConversationPageDto Conversation);
public record ConversationPageDto(string Id, string FirstParticipantId, string SecondParticipantId,
    UserPageDto FirstParticipant, UserPageDto SecondParticipant);

public record NotificationPageDto(string Id, string Message, string UserId, bool IsRead, UserPageDto User);
public record ConnectRequestPageDto(string Id, string InviterId, string InviteeId, UserPageDto Inviter, UserPageDto Invitee);