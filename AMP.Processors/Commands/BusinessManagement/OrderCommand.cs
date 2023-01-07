using AMP.Processors.Commands.UserManagement;

namespace AMP.Processors.Commands.BusinessManagement
{
    public class OrderCommand
    {
        public string ReferenceNo { get; set; }
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string UserId { get; set; }
        public string ServiceId { get; set; }
        public string? ArtisanId { get; set; }
        //public bool IsComplete { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; } 
        public Urgency Urgency { get; set; }
        public ScopeOfWork Scope { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime PreferredStartDate { get; set; }
        public DateTime PreferredCompletionDate { get; set; }
        public AddressCommand WorkAddress { get; set; }
    }
}