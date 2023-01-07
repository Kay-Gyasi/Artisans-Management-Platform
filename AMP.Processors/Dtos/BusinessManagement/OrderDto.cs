using AMP.Processors.Dtos.UserManagement;

namespace AMP.Processors.Dtos.BusinessManagement
{
    public class OrderDto
    {
        public string ReferenceNo { get; set; }
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string? ArtisanId { get; set; }
        public string ServiceId { get; set; }
        //public bool IsComplete { get; set; }
        public bool IsArtisanComplete { get; set; }
        public bool IsRequestAccepted { get; set; }
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