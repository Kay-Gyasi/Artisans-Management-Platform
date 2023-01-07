using AMP.Domain.Entities.Base;
using AMP.Domain.Entities.UserManagement;

namespace AMP.Domain.Entities.BusinessManagement
{
    public sealed class Rating : EntityBase
    {
        public string ArtisanId { get; private set; }
        public string CustomerId { get; private set; }
        public int Votes { get; private set; }
        public string Description { get; private set; }
        public Artisan Artisan { get; private set; }
        public Customer Customer { get; private set; }

        private Rating(){}

        private Rating(string customerId, string artisanId)
        {
            CustomerId = customerId;
            ArtisanId = artisanId;
        }

        public static Rating Create(string customerId, string artisanId) 
            => new Rating(customerId, artisanId);

        public Rating ForCustomerWithId(string customerId)
        {
            CustomerId = customerId;
            return this;
        }

        public Rating ForArtisanWithId(string artisanId)
        {
            ArtisanId = artisanId;
            return this;
        }

        public Rating WithVotes(int votes)
        {
            if (votes < 0)
                Votes = 0;
            else if (votes > 5)
                Votes = 5;
            else
                Votes = votes;
            
            return this;
        }

        public Rating WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public Rating ForArtisan(Artisan artisan)
        {
            Artisan = artisan;
            return this;
        }

        public Rating ForCustomer(Customer customer)
        {
            Customer = customer;
            return this;
        }
    }
}