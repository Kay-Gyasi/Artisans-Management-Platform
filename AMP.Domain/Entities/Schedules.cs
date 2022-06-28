using System;
using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities
{
    public class Schedules : EntityBase
    {
        public int OrderId { get; private set; }
        public int ArtisanId { get; private set; }
        public DateTime Date { get; private set; }
        public DateTime Time { get; private set; }
        public string Description { get; private set; }
        public Orders Order { get; private set; }
        public Artisans Artisan { get; private set; }

        private Schedules(){}

        private Schedules(int orderId, int artisanId)
        {
            OrderId = orderId;
            ArtisanId = artisanId;
        }

        public Schedules Create(int orderId, int artisanId)
        {
            return new Schedules(orderId, artisanId);
        }

        public Schedules OnOrderWithId(int orderId)
        {
            OrderId = orderId;
            return this;
        }

        public Schedules ForArtisanWithId(int artisanId)
        {
            ArtisanId = artisanId;
            return this;
        }

        public Schedules On(DateTime date)
        {
            Date = date;
            return this;
        }

        public Schedules At(DateTime time)
        {
            Time = time;
            return this;
        }

        public Schedules WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public Schedules OnOrder(Orders order)
        {
            Order = order;
            return this;
        }

        public Schedules ForArtisan(Artisans artisan)
        {
            Artisan = artisan;
            return this;
        }

        public Schedules CreatedOn(DateTime date)
        {
            DateCreated = date;
            return this;
        }
    }
}