using AMP.Domain.Enums;

namespace AMP.Processors.Dtos
{
    public class PaymentDto
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; } 
        public decimal AmountPaid { get; set; }
        public PaymentStatus Status { get; set; }
        public CustomerDto Customer { get; set; }
        public OrderDto Order { get; set; }
    }
}