using System;
using AMP.Domain.Entities.Base;
using AMP.Domain.Enums;

namespace AMP.Domain.Entities
{
    public class Payments : EntityBase
    {
        public int CustomerId { get; private set; }
        public int OrderId { get; private set; }
        public decimal AmountPaid { get; private set; }
        public PaymentStatus Status { get; private set; }
        public Customers Customer { get; private set; }
        public Orders Order { get; private set; }

        private Payments(){}

        private Payments(int customerId, int orderId)
        {
            CustomerId = customerId;
            OrderId = orderId;
        }

        public static Payments Create(int customerId, int orderId)
        {
            return new Payments(customerId, orderId);
        }

        public Payments ByCustomerWithId(int customerId)
        {
            CustomerId = customerId;
            return this;
        }

        public Payments OnOrderWithId(int orderId)
        {
            OrderId = orderId;
            return this;
        }

        public Payments WithAmountPaid(decimal amount)
        {
            AmountPaid = amount;
            return this;
        }

        public Payments WithStatus(PaymentStatus status)
        {
            Status = status;
            return this;
        }

        public Payments ByCustomer(Customers customer)
        {
            Customer = customer;
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
    }
}