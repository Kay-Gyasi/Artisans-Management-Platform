using AMP.Domain.Enums;

namespace AMP.Processors.Commands
{
    public class PaymentCommand
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public int OrderId { get; set; }
        public decimal AmountPaid { get; set; }
    }
}