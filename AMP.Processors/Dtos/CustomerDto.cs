using System.Collections.Generic;

namespace AMP.Processors.Dtos
{
    public class CustomerDto
    {
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public List<RatingDto> Ratings { get; set; }
        public List<OrderDto> Orders { get; set; }
        public List<DisputeDto> Disputes { get; set; }
        public List<PaymentDto> Payments { get; set; }
    }
}