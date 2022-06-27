using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities
{
    public class Proposals : EntityBase
    {
        public int OrderId { get; private set; }
        public int ArtisanId { get; private set; }
        public bool IsAccepted { get; private set; }
        public Orders Order { get; private set; }
        public Artisans Artisan { get; private set; }

        private Proposals(){}

        private Proposals(int orderId, int artisanId)
        {
            OrderId = orderId;
            ArtisanId = artisanId;
        }

        public Proposals Create(int orderId, int artisanId)
        {
            return new Proposals(orderId, artisanId);
        }

        public Proposals ForOrderWithId(int orderId)
        {
            OrderId = orderId;
            return this;
        }

        public Proposals ByArtisanWithId(int artisanId)
        {
            ArtisanId = artisanId;
            return this;
        }

        public Proposals IsAcceptedd(bool isAccepted)
        {
            IsAccepted = isAccepted;
            return this;
        }

        public Proposals ForOrder(Orders order)
        {
            Order = order;
            return this;
        }

        public Proposals ByArtisan(Artisans artisan)
        {
            Artisan = artisan;
            return this;
        }
    }
}