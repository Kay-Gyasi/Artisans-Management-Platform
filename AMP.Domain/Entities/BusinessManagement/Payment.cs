using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities.BusinessManagement
{
    public sealed class Payment : EntityBase
    {
        public string OrderId { get; private set; }
        public decimal AmountPaid { get; private set; }
        public bool IsVerified { get; private set; }
        public bool IsForwarded { get; private set; }
        public string TransactionReference { get; private set; }
        public string Reference { get; private set; }
        public Order Order { get; private set; }

        private Payment(){}

        private Payment(string orderId)
        {
            OrderId = orderId;
        }

        public static Payment Create(string orderId) 
            => new Payment(orderId);

        public Payment OnOrderWithId(string orderId)
        {
            OrderId = orderId;
            return this;
        }

        public Payment WithAmountPaid(decimal amount)
        {
            AmountPaid = amount;
            return this;
        }

        public Payment HasBeenVerified(bool isVerified)
        {
            IsVerified = isVerified;
            return this;
        }
        
        public Payment WithTransactionReference(string trxRef)
        {
            TransactionReference = trxRef;
            return this;
        }
        
        public Payment WithReference(string reference)
        {
            Reference = reference;
            return this;
        }
        
        public Payment HasBeenForwarded(bool isForwarded)
        {
            IsForwarded = isForwarded;
            return this;
        }

        public Payment OnOrder(Order order)
        {
            Order = order;
            return this;
        }
    }
}