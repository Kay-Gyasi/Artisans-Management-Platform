using System;
using AMP.Domain.Entities.Base;
using AMP.Domain.Entities.UserManagement;
using AMP.Domain.Enums;

namespace AMP.Domain.Entities.BusinessManagement
{
    public sealed class Dispute : Entity
    {
        public string CustomerId { get; private set; }
        public string OrderId { get; private set; }
        public string Details { get; private set; }
        public DisputeStatus Status { get; private set; }
        public Customer Customer { get; private set; }
        public Order Order { get; private set; }

        private Dispute(){}

        private Dispute(string customerId, string orderId)
        {
            CustomerId = customerId;
            OrderId = orderId;
        }

        public static Dispute Create(string customerId, string orderId) 
            => new Dispute(customerId, orderId);

        public Dispute ByCustomerWithId(string customerId)
        {
            CustomerId = customerId;
            return this;
        }

        public Dispute AgainstOrderWithId(string orderId)
        {
            OrderId = orderId;
            return this;
        }

        public Dispute WithDetails(string details)
        {
            Details = details;
            return this;
        }

        public Dispute ByCustomer(Customer customer)
        {
            Customer = customer;
            return this;
        }

        public Dispute AgainstOrder(Order order)
        {
            Order = order;
            return this;
        }

        public Dispute WithStatus(DisputeStatus status)
        {
            Status = status;
            return this;
        }

        public Dispute CreatedOn(DateTime date)
        {
            DateCreated = date;
            return this;
        }
    }
}