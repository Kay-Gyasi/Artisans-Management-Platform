using AMP.Domain.Enums;

namespace AMP.Processors.Dtos
{
    public class DisputeDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public string Details { get; set; }
        public DisputeStatus Status { get; set; }
        public CustomerDto Customer { get; set; }
        public OrderDto Order { get; set; }
    }
}