using AMP.Domain.Enums;

namespace AMP.Processors.Commands
{
    public class PaymentCommand
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public PaymentStatus Status { get; set; }
        public int OrderId { get; set; }
        public decimal AmountPaid { get; set; }
    }
}