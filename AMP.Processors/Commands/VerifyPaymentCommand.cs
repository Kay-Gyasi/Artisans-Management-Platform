namespace AMP.Processors.Commands
{
    public class VerifyPaymentCommand
    {
        public string Reference { get; set; }
        public string TransactionReference { get; set; }
    }
}