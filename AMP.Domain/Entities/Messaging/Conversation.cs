using System;
using System.Collections.Generic;
using AMP.Domain.Entities.Base;
using AMP.Domain.Entities.UserManagement;

namespace AMP.Domain.Entities.Messaging;

public class Conversation : EntityBase
{
    private Conversation(string firstParticipantId, string secondParticipantId)
    {
        FirstParticipantId = firstParticipantId;
        SecondParticipantId = secondParticipantId;
    }
    public string FirstParticipantId { get; private set; }
    public string SecondParticipantId { get; private set; }
    
    public User FirstParticipant { get; private set; }
    public User SecondParticipant { get; private set; }
    
    private readonly List<ChatMessage> _messages = new();
    public IReadOnlyList<ChatMessage> Messages => _messages.AsReadOnly();

    public static Conversation Create(string firstParticipantId, string secondParticipantId)
    {
        return new Conversation(firstParticipantId, secondParticipantId);
    }

    public Conversation Between(string firstParticipantId, string secondParticipantId)
    {
        FirstParticipantId = firstParticipantId;
        SecondParticipantId = secondParticipantId;
        return this;
    }

    public Conversation IsModified()
    {
        DateModified = DateTime.UtcNow;
        return this;
    }
}