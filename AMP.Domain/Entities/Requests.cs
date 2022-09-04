using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities
{
    public class Requests : EntityBase
    {
        public string CustomerId { get; private set; }
        public string ArtisanId { get; private set; }
        public string OrderId { get; private set; }
        public Customers Customer { get; private set; }
        public Artisans Artisan { get; private set; }
        public Orders Order { get; private set; }

        private Requests(string customerId, string artisanId, string orderId)
        {
            CustomerId = customerId;
            ArtisanId = artisanId;
            OrderId = orderId;
        }

        public static Requests Create(string customerId, string artisanId, string orderId)
        {
            return new Requests(customerId, artisanId, orderId);
        }

        public Requests WithId(string id)
        {
            Id = id;
            return this;
        }
    }
}