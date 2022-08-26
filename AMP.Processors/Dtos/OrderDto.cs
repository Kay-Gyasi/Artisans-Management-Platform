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
        public int ServiceId { get; set; }
        public int? PaymentId { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; } // To be set by approved artisan
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