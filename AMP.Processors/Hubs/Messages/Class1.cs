namespace AMP.Processors.Hubs.Messages;

public record CountMessage(DataCountType Type, int Value);