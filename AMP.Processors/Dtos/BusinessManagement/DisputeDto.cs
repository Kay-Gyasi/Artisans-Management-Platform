using AMP.Processors.Dtos.UserManagement;

namespace AMP.Processors.Dtos.BusinessManagement
{
    public class DisputeDto
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string OrderId { get; set; }
        public string Details { get; set; }
        public DisputeStatus Status { get; set; }
        public CustomerDto Customer { get; set; }
        public OrderDto Order { get; set; }
    }
}