using System;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects;

namespace AMP.Processors.Commands
{
    public class OrderCommand
    {
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int? PaymentId { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; } // To be set by approved artisan (TODO::use a different model to set the cost)
        public Urgency Urgency { get; set; }
        public DateTime PreferredDate { get; set; }
        public Address WorkAddress { get; set; }
        public PaymentCommand Payment { get; set; }
    }
}