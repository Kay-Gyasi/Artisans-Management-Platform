namespace AMP.Processors.PageDtos
{
    public class PaymentPageDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal AmountPaid { get; set; }
        public bool IsVerified { get; set; }
        public bool IsForwarded { get; set; }
        public string TransactionReference { get; set; }
        public string Reference { get; set; }
        public OrderPageDto Order { get; set; }
    }
}