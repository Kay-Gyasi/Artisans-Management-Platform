using System;
using System.Data;
using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities
{
    public sealed class Ratings : EntityBase
    {
        public string ArtisanId { get; private set; }
        public string CustomerId { get; private set; }
        public int Votes { get; private set; }
        public string Description { get; private set; }
        public Artisans Artisan { get; private set; }
        public Customers Customer { get; private set; }

        private Ratings(){}

        private Ratings(string customerId, string artisanId)
        {
            CustomerId = customerId;
            ArtisanId = artisanId;
        }

        public static Ratings Create(string customerId, string artisanId) 
            => new Ratings(customerId, artisanId);

        public Ratings ForCustomerWithId(string customerId)
        {
            CustomerId = customerId;
            return this;
        }

        public Ratings ForArtisanWithId(string artisanId)
        {
            ArtisanId = artisanId;
            return this;
        }

        public Ratings WithVotes(int votes)
        {
            if (votes < 0)
                Votes = 0;
            else if (votes > 5)
                Votes = 5;
            else
                Votes = votes;
            
            return this;
        }

        public Ratings WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public Ratings ForArtisan(Artisans artisan)
        {
            Artisan = artisan;
            return this;
        }

        public Ratings ForCustomer(Customers customer)
        {
            Customer = customer;
            return this;
        }

        public Ratings CreatedOn(DateTime date)
        {
            DateCreated = date;
            return this;
        }
        
        public Ratings SetLastModified()
        {
            DateModified = DateTime.UtcNow;
            return this;
        }
    }
}