using System.Collections.Generic;

namespace AMP.Processors.Dtos
{
    public class ArtisanDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string BusinessName { get; set; }
        public string Description { get; set; }
        public bool IsVerified { get; set; }
        public bool IsApproved { get; set; }
        public double Rating { get; set; }
        public int NoOfReviews { get; set; }
        public int NoOfOrders { get; set; }
        public UserDto User { get; set; }
        public List<ServiceDto> Services { get; set; }
        public List<RatingDto> Ratings { get; set; }
        public List<OrderDto> Orders { get; set; }
        public List<DisputeDto> Disputes { get; set; }
    }
}