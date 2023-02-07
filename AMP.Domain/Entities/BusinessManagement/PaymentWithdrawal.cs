using AMP.Domain.Entities.Base;
using AMP.Domain.Entities.UserManagement;
using AMP.Domain.ValueObjects;

namespace AMP.Domain.Entities.BusinessManagement;

public class PaymentWithdrawal : Entity
{
    private PaymentWithdrawal(string userId, decimal amount)
    {
        UserId = userId;
        Amount = amount;
    }
    
    private PaymentWithdrawal(){}
    
    public string UserId { get; }
    public decimal Amount { get; }
    public User User { get; private set; }
    public MomoTransfer MomoTransfer { get; private set; }
    public bool IsMomo => !string.IsNullOrEmpty(MomoTransfer.Number);

    public static PaymentWithdrawal Create(string userId, decimal amount)
        => new (userId, amount);

    public PaymentWithdrawal SendByMomo(MomoTransfer momo)
    {
        MomoTransfer = momo;
        return this;
    }
}