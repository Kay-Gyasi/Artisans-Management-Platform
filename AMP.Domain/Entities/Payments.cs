using System;
using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities
{
    public sealed class Payments : EntityBase
    {
        public string OrderId { get; private set; }
        public decimal AmountPaid { get; private set; }
        public bool IsVerified { get; private set; }
        public bool IsForwarded { get; private set; }
        public string TransactionReference { get; private set; }
        public string Reference { get; private set; }
        public Orders Order { get; private set; }

        private Payments(){}

        private Payments(string orderId)
        {
            OrderId = orderId;
        }

        public static Payments Create(string orderId) 
            => new Payments(orderId);

        public Payments OnOrderWithId(string orderId)
        {
            OrderId = orderId;
            return this;
        }

        public Payments WithAmountPaid(decimal amount)
        {
            AmountPaid = amount;
            return this;
        }

        public Payments HasBeenVerified(bool isVerified)
        {
            IsVerified = isVerified;
            return this;
        }
        
        public Payments WithTransactionReference(string trxRef)
        {
            TransactionReference = trxRef;
            return this;
        }
        
        public Payments WithReference(string reference)
        {
            Reference = reference;
            return this;
        }
        
        public Payments HasBeenForwarded(bool isForwarded)
        {
            IsForwarded = isForwarded;
            return this;
        }

        public Payments OnOrder(Orders order)
        {
            Order = order;
            return this;
        }

        public Payments CreatedOn(DateTime date)
        {
            DateCreated = date;
            return this;
        }

        public Payments LastModifiedOn()
        {
            DateModified = DateTime.UtcNow;
            return this;
        }
    }
}