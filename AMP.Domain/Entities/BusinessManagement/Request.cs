using AMP.Domain.Entities.Base;
using AMP.Domain.Entities.UserManagement;

namespace AMP.Domain.Entities.BusinessManagement
{
    public sealed class Request : Entity
    {
        public string CustomerId { get; }
        public string ArtisanId { get; }
        public string OrderId { get; }
        public Customer Customer { get; private set; }
        public Artisan Artisan { get; private set; }
        public Order Order { get; private set; }

        private Request(string customerId, string artisanId, string orderId)
        {
            CustomerId = customerId;
            ArtisanId = artisanId;
            OrderId = orderId;
        }

        public static Request Create(string customerId, string artisanId, string orderId) 
            => new Request(customerId, artisanId, orderId);
    }
}