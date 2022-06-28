using AMP.Domain.Enums;

namespace AMP.Processors.PageDtos
{
    public class PaymentPageDto
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public decimal AmountPaid { get; set; }
        public PaymentStatus Status { get; set; }
        public CustomerPageDto Customer { get; set; }
        public OrderPageDto Order { get; set; }
    }
}