namespace AMP.Processors.Commands
{
    public class PaymentCommand
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public decimal AmountPaid { get; set; }
    }
}