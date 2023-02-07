using AMP.Domain.Entities.Base;
using AMP.Domain.Entities.UserManagement;

namespace AMP.Domain.Entities.Messaging;

public class Notification : Entity
{
    private Notification(string message, string userId, bool isRead)
    {
        Message = message;
        UserId = userId;
        IsRead = isRead;
    }
    
    public string Message { get; private set; }
    public string UserId { get; private set; }
    public bool IsRead { get; private set; }
    public User User { get; set; }

    public static Notification Create(string message, string userId, bool isRead = false)
    {
        return new Notification(message, userId, isRead);
    }

    public Notification WithMessage(string message)
    {
        Message = message;
        return this;
    }

    public Notification ForUser(string userId)
    {
        UserId = userId;
        return this;
    }

    public Notification IsSeen()
    {
        IsRead = true;
        return this;
    }
}