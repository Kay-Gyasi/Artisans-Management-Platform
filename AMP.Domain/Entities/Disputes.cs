using System;
using AMP.Domain.Entities.Base;
using AMP.Domain.Enums;

namespace AMP.Domain.Entities
{
    public class Disputes : EntityBase
    {
        public int CustomerId { get; private set; }
        public int OrderId { get; private set; }
        public string Details { get; private set; }
        public DisputeStatus Status { get; private set; }
        public Customers Customer { get; private set; }
        public Orders Order { get; private set; }

        private Disputes(){}

        private Disputes(int customerId, int orderId)
        {
            CustomerId = customerId;
            OrderId = orderId;
        }

        public static Disputes Create(int customerId, int orderId)
        {
            return new Disputes(customerId, orderId);
        }

        public Disputes ByCustomerWithId(int customerId)
        {
            CustomerId = customerId;
            return this;
        }

        public Disputes AgainstOrderWithId(int orderId)
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

        public Disputes WithId(int id)
        {
            Id = id;
            return this;
        }
    }
}