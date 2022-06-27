using System.Data;
using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities
{
    public class Ratings : EntityBase
    {
        public int ArtisanId { get; private set; }
        public int CustomerId { get; private set; }
        public int Votes { get; private set; }
        public string Description { get; private set; }
        public Artisans Artisan { get; private set; }
        public Customers Customer { get; private set; }

        private Ratings(){}

        private Ratings(int customerId, int artisanId)
        {
            CustomerId = customerId;
            ArtisanId = artisanId;
        }

        public Ratings ForCustomerWithId(int customerId)
        {
            CustomerId = customerId;
            return this;
        }

        public Ratings ForArtisanWithId(int artisanId)
        {
            ArtisanId = artisanId;
            return this;
        }

        public Ratings WithVotes(int votes)
        {
            if (votes < 0)
            {
                Votes = 0;
                return this;
            }

            if (votes > 5)
            {
                Votes = 5;
                return this;
            }

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
    }
}