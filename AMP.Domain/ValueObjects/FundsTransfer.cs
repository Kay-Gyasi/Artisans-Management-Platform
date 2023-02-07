using System;
using System.Collections.Generic;
using AMP.Domain.ValueObjects.Base;

namespace AMP.Domain.ValueObjects;

public class FundsTransfer : ValueObject
{
    private FundsTransfer(int? recipientId, string recipientCode,
        DateTime? createdAt, DateTime? updatedAt)
    {
        RecipientId = recipientId;
        RecipientCode = recipientCode;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    private FundsTransfer() { }

    public static FundsTransfer Create(int? recipientId, string recipientCode,
        DateTime? createdAt, DateTime? updatedAt)
    {
        return new FundsTransfer(recipientId, recipientCode, createdAt, updatedAt);
    }
    
    public int? RecipientId { get; private set; }
    public string RecipientCode { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return RecipientId;
        yield return RecipientCode;
    }
}