using AMP.Domain.Enums;

namespace AMP.Processors.Dtos
{
    public class PaymentDto
    {
        public string Id { get; set; }
        public string OrderId { get; set; } 
        public decimal AmountPaid { get; set; }
        public bool IsVerified { get; set; }
        public bool IsForwarded { get; set; }
        public string TransactionReference { get; set; }
        public string Reference { get; set; }

        public OrderDto Order { get; set; }
    }
}