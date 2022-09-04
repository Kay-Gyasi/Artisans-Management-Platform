using System;
using AMP.Domain.Entities.Base;
using AMP.Domain.Enums;

namespace AMP.Domain.Entities
{
    public class Disputes : EntityBase
    {
        public string CustomerId { get; private set; }
        public string OrderId { get; private set; }
        public string Details { get; private set; }
        public DisputeStatus Status { get; private set; }
        public Customers Customer { get; private set; }
        public Orders Order { get; private set; }

        private Disputes(){}

        private Disputes(string customerId, string orderId)
        {
            CustomerId = customerId;
            OrderId = orderId;
        }

        public static Disputes Create(string customerId, string orderId)
        {
            return new Disputes(customerId, orderId);
        }

        public Disputes ByCustomerWithId(string customerId)
        {
            CustomerId = customerId;
            return this;
        }

        public Disputes AgainstOrderWithId(string orderId)
        {
            OrderId = orderId;
            return this;
        }

        public Disputes WithDetails(string details)
        {
            Details = details;
            return this;
        }

        public Disputes ByCustomer(Customers customer)
        {
            Customer = customer;
            return this;
        }

        public Disputes AgainstOrder(Orders order)
        {
            Order = order;
            return this;
        }

        public Disputes WithStatus(DisputeStatus status)
        {
            Status = status;
            return this;
        }

        public Disputes CreatedOn(DateTime date)
        {
            DateCreated = date;
            return this;
        }

        public Disputes WithId(string id)
        {
            Id = id;
            return this;
        }
    }
}