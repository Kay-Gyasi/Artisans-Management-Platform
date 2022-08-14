using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities
{
    public class Requests : EntityBase
    {
        public int CustomerId { get; private set; }
        public int ArtisanId { get; private set; }
        public int OrderId { get; private set; }
        public Customers Customer { get; private set; }
        public Artisans Artisan { get; private set; }
        public Orders Order { get; private set; }

        private Requests(int customerId, int artisanId, int orderId)
        {
            CustomerId = customerId;
            ArtisanId = artisanId;
            OrderId = orderId;
        }

        public static Requests Create(int customerId, int artisanId, int orderId)
        {
            return new Requests(customerId, artisanId, orderId);
        }
    }
}