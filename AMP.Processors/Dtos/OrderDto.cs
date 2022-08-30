using System;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects;

namespace AMP.Processors.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? ArtisanId { get; set; }
        //public bool IsComplete { get; set; }
        public bool IsArtisanComplete { get; set; }
        public bool IsRequestAccepted { get; set; }
        public int ServiceId { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal PaymentMade { get; set; }
        public Urgency Urgency { get; set; }
        public ScopeOfWork Scope { get; private set; }
        public OrderStatus Status { get; set; }
        public DateTime PreferredStartDate { get; set; }
        public DateTime PreferredCompletionDate { get; set; }
        public ArtisanDto Artisan { get; set; }
        public AddressDto WorkAddress { get; set; }
        public CustomerDto Customer { get; set; }
        public ServiceDto Service { get; set; }
        public PaymentDto Payment { get; set; }
    }
}