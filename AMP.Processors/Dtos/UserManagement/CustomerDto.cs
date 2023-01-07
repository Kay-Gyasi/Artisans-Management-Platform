using AMP.Processors.Dtos.BusinessManagement;

namespace AMP.Processors.Dtos.UserManagement
{
    public class CustomerDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public UserDto User { get; set; }
        public List<RatingDto> Ratings { get; set; }
        public List<OrderDto> Orders { get; set; }
        public List<DisputeDto> Disputes { get; set; }
        public List<PaymentDto> Payments { get; set; }
    }
}