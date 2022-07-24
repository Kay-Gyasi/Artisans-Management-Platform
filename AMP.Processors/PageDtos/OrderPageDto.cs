using System;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects;

namespace AMP.Processors.PageDtos
{
    public class OrderPageDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; } // To be set by approved artisan
        public Urgency Urgency { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime PreferredDate { get; set; }
        public CustomerPageDto Customer { get; set; }
        public ServicePageDto Service { get; set; }
    }
}