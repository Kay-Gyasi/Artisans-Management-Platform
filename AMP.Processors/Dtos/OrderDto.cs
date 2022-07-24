using System;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects;

namespace AMP.Processors.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int PaymentId { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; } // To be set by approved artisan
        public Urgency Urgency { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime PreferredDate { get; set; }
        public Address WorkAddress { get; set; }
        public CustomerDto Customer { get; set; }
        public ServiceDto Service { get; set; }
        public PaymentDto Payment { get; set; }
    }
}