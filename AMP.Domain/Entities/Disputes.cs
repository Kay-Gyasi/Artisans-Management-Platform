using System;
using AMP.Domain.Entities.Base;
using AMP.Domain.Enums;

namespace AMP.Domain.Entities
{
    public class Disputes : EntityBase
    {
            public int CustomerId { get; private set; }
            public int ArtisanId { get; private set; }
            public string Details { get; private set; }
            public DisputeStatus Status { get; private set; }
            public Customers Customer { get; private set; }
            public Artisans Artisan { get; private set; }

        private Disputes(){}

        private Disputes(int customerId, int artisanId)
        {
            CustomerId = customerId;
            ArtisanId = artisanId;
        }

        public Disputes Create(int customerId, int artisanId)
        {
            return new Disputes(customerId, artisanId);
        }

        public Disputes ByCustomerWithId(int customerId)
        {
            CustomerId = customerId;
            return this;
        }

        public Disputes AgainstArtisanWithId(int artisanId)
        {
            ArtisanId = artisanId;
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

        public Disputes AgainstArtisan(Artisans artisan)
        {
            Artisan = artisan;
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
    }
}