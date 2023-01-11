using AMP.Processors.Dtos.Messaging;

namespace AMP.Processors.Hubs.Messages;

public record CountMessage(DataCountType Type, int Value, string ConversationId = null);