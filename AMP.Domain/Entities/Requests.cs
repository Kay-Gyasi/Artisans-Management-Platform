using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities
{
    public sealed class Requests : EntityBase
    {
        public string CustomerId { get; }
        public string ArtisanId { get; }
        public string OrderId { get; }
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
            => new Requests(customerId, artisanId, orderId);
    }
}