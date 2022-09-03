using System;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects;

namespace AMP.Processors.PageDtos
{
    public class OrderPageDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? ArtisanId { get; set; }
        public bool IsRequestAccepted { get; set; }
        public string Description { get; set; }
        public Address WorkAddress { get; set; }
        public ServicePageDto Service { get; set; }
    }
}