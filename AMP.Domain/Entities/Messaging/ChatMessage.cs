using AMP.Domain.Entities.Base;
using AMP.Domain.Entities.UserManagement;

namespace AMP.Domain.Entities.Messaging;

public class ChatMessage : Entity
{
    private ChatMessage(string conversationId, string message)
    {
        ConversationId = conversationId;
        Message = message;
    }

    public string SenderId { get; private set; }
    public string ReceiverId { get; private set; }
    public string Message { get; private set; }
    public string ConversationId { get; private set; }
    public bool IsSeen { get; private set; }
    public User Sender { get; private set; }
    public User Receiver { get; private set; }
    public Conversation Conversation { get; private set; }

    public static ChatMessage Create(string conversationId, string message)
    {
        return new ChatMessage(conversationId, message);
    }

    public ChatMessage SentBy(string senderId)
    {
        SenderId = senderId;
        return this;
    }

    public ChatMessage To(string receiverId)
    {
        ReceiverId = receiverId;
        return this;
    }

    public ChatMessage WithMessage(string message)
    {
        Message = message;
        return this;
    }

    public ChatMessage IsRead()
    {
        IsSeen = true;
        return this;
    }

    public ChatMessage ForConversation(string conversationId)
    {
        ConversationId = conversationId;
        return this;
    }
}